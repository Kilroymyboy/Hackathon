using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script tiles any background GameObject it's attached to. New background GameObjects are copied/created
as the player moves closer to the visible edge of the current background GameObject. This tiling script also destroys
copies once they've fallen outside of the camera's visibility. This creates the ability to tile infinitely, in either direction,
without bloating memory with unwanted copies.
*/
#endregion

[RequireComponent(typeof(SpriteRenderer))]

public class BackgroundTile : MonoBehaviour
{
    //Stores an offset to copy a tile before it reaches the exact point where its edge meets the camera's edge
    private const int LookAheadOffset = 2;

    //Bools used to determine if this tile has a copy to the right/left of it
    //Important to avoid instantiating the same tile multiple times
    public bool HasRightCopy = false;
    public bool HasLeftCopy = false;

    //Stores width of the camera viewport
    private float CamwidthX;

    //Stores width of the sprite used as a tile
    private float SpriteWidthX;

    //Stores references to tile it was copied from and/or copied to
    //Important for setting HasRightCopy/HasLeftCopy especially when a copy has been destroyed, potentially nulling these references
    Transform CopiedTo;
    Transform CopiedFrom;

	void Start()
	{
        SpriteWidthX = GetComponent<SpriteRenderer>().bounds.size.x;
        CamwidthX = Camera.main.orthographicSize * Camera.main.aspect;
	}

    //Controls instatiation and destroying of tile copies
	void Update()
	{
        float spriteRightEdge = transform.position.x + SpriteWidthX / 2;
        float spriteLeftEdge = transform.position.x - SpriteWidthX / 2;

        float camRightEdge = Camera.main.transform.position.x + CamwidthX;
        float camLeftEdge = Camera.main.transform.position.x - CamwidthX;

        if (camRightEdge + LookAheadOffset > spriteRightEdge)
            if (!HasRightCopy)
                MakeCopy(CopyTo.right);

        if (camLeftEdge - LookAheadOffset < spriteLeftEdge)
            if (!HasLeftCopy)
                MakeCopy(CopyTo.left);

        DestroyIfInvisible(camRightEdge, camLeftEdge, spriteRightEdge, spriteLeftEdge);
    }

    //Makes a copy of the tile when called in Update()
    private void MakeCopy(CopyTo side)
    {
        Vector3 copyPosition = new Vector3(transform.position.x + SpriteWidthX * (int)side, transform.position.y, transform.position.z);

        CopiedTo = (Transform)Instantiate(transform, copyPosition, transform.rotation);
        CopiedTo.GetComponent<BackgroundTile>().CopiedFrom = this.transform;
        CopiedTo.parent = this.transform.parent;

        if (side == CopyTo.right)
            this.HasRightCopy = CopiedTo.GetComponent<BackgroundTile>().HasLeftCopy = true;

        else if (side == CopyTo.left)
            this.HasLeftCopy = CopiedTo.GetComponent<BackgroundTile>().HasRightCopy = true;
    }

    //Destroy the copied tile when invisible to the camera and remove CopiedTo and CopiedFrom references to the tile
    private void DestroyIfInvisible(float camRightEdge, float camLeftEdge, float spriteRightEdge, float spriteLeftEdge)
    {
        bool isSpriteInvisibleToRightOfCamera = (spriteLeftEdge - camRightEdge > SpriteWidthX);
        bool isSpriteInvisibleToleftOfCamera = (camLeftEdge - spriteRightEdge > SpriteWidthX);

        if (isSpriteInvisibleToRightOfCamera)
        {
            if (CopiedFrom != null)
                CopiedFrom.GetComponent<BackgroundTile>().HasRightCopy = false;

            if (CopiedTo != null)
                CopiedTo.GetComponent<BackgroundTile>().HasRightCopy = false;

            GameObject.Destroy(gameObject);
        }
        else if (isSpriteInvisibleToleftOfCamera)
        {
            if (CopiedFrom != null)
                CopiedFrom.GetComponent<BackgroundTile>().HasLeftCopy = false;

            if (CopiedTo != null)
                CopiedTo.GetComponent<BackgroundTile>().HasLeftCopy = false;

            GameObject.Destroy(gameObject);
        }
    }

    private enum CopyTo
    {
        right = 1,
        left = -1
    }
}
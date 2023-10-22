using UnityEngine;

public class FlashlightRaycaster : MonoBehaviour
{
    public LayerMask raycastOnLayer;
    public Transform movementTarget;
    
    private RaycastHit[] hits;
    private TimeoutTile hoveredTile;
    
    // Start is called before the first frame update
    void Start()
    {
        hits = new RaycastHit[1];   // only one at a time
    }

    private void OnDisable()
    {
        if (hoveredTile != null) hoveredTile.OnFlashlightExited(null);
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(transform.position, transform.forward);
        var hitCount = Physics.RaycastNonAlloc(ray, hits, 50f, raycastOnLayer);
        if (hitCount == 0)
        {
            IsHovering(null);
            return;
        }
        
        // if the surface is looking "up"
        if (Vector3.Dot(hits[0].normal, Vector3.up) > 0.8f)
        {
            movementTarget.position = hits[0].point;
        }
        
        var timeoutTile = hits[0].collider.GetComponent<TimeoutTile>();
        // timeoutTile can be null
        IsHovering(timeoutTile);
    }

    private void IsHovering(TimeoutTile tile)
    {
        // if it's the same, don't do anything
        if (hoveredTile == tile) return;    

        // if we were hovering a tile, Exit this tile
        if (hoveredTile != null) hoveredTile.OnFlashlightExited(null);

        // if we're entering a new tile, call Enter
        if (tile != null) tile.OnFlashlightEntered(null);
        
        // save new tile as hovered
        hoveredTile = tile;
    }
}

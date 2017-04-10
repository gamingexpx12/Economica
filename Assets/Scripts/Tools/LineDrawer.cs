using UnityEngine;

[RequireComponent(typeof(Ghosting))]
public class LineDrawer: MonoBehaviour
{
    /// <summary>
    /// Distance from furthest point in tiles.
    /// </summary>
    public int distanceTile;
    public int distanceReal;
    public int numInstances;
    public int grid = 2;
    public Vector3 inTilePositon;
    public Vector3 snappedEnd;
    public Vector3 debugEnd;
    public Vector3 localEnd;

    public Vector3 begin
    {
        get { return _begin; }
        set { _begin = value; }
        
    }
    public Vector3 end
    {
        get { return _end; }
        set { _end = value; }

    }

    public GameObject model
    {
        get { return _model; }
        set { _model = value; }
    }

    Vector3 _begin;
    Vector3 _end;
    Quaternion _direction;
    Vector3[] _ghosts;
    Ghosting _ghoster;
    GameObject _model;

    LocationBasedFunctions sameFuncs;
    LocationBasedFunctions diffFuncs;
    LocationBasedFunctions locationFuncs;

    public LineData MakeLine(Vector3 begin, Vector3 end)
    {
        LineData line = new LineData();
        //Pick diffrent functions if were only drawing one instance.
        locationFuncs = SharedLibrary.VectorLocationEqual(begin, end) ? sameFuncs : diffFuncs;

        localEnd = end - begin;
        int furthestAxis = Mathf.RoundToInt(Mathf.Max(Mathf.Abs(localEnd.x), Mathf.Abs(localEnd.z)));
        distanceTile = furthestAxis / grid;
        distanceReal = furthestAxis;
        snappedEnd = begin + _SnapToCardinal(localEnd, distanceReal);

        locationFuncs._SetInstances(begin, snappedEnd);
        numInstances = _ghosts.Length;
        _direction = locationFuncs._GetDirection(begin, snappedEnd);
        _ghoster.Ghost(_model, _direction, _ghosts);

        line.instances = _ghosts;
        line.direction = _direction;
        return line;
    }

    private void Start()
    {
        sameFuncs = new SameLocation(this);
        diffFuncs = new DifferentLocation(this);
        locationFuncs = sameFuncs;

        _ghoster = GetComponent<Ghosting>();
    }

    private void Update()
    {
        return;
        //Pick diffrent functions if were only drawing one instance.
        locationFuncs = SharedLibrary.VectorLocationEqual(_begin, _end) ? sameFuncs : diffFuncs;

        localEnd = _end - _begin;
        int furthestAxis = Mathf.RoundToInt(Mathf.Max(Mathf.Abs(localEnd.x), Mathf.Abs(localEnd.z)));
        distanceTile = furthestAxis / grid;
        distanceReal = furthestAxis;
        snappedEnd = _begin + _SnapToCardinal(localEnd, distanceReal);

        locationFuncs._SetInstances(_begin, snappedEnd);
        numInstances = _ghosts.Length;
        _direction = locationFuncs._GetDirection(_begin, snappedEnd);
        _ghoster.Ghost(_model, _direction, _ghosts);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (_ghosts == null)
        {
            return;
        }
        Gizmos.DrawCube(begin, Vector3.one);
        Gizmos.DrawCube(end, Vector3.one);
        Gizmos.DrawLine(begin, end);
        
        for (int i = 0; i < _ghosts.Length; i++)
        {
            Gizmos.DrawSphere(_ghosts[i], 0.3f);
        }
    }

    private Vector3 _SnapToCardinal(Vector3 vector, int distance)
    {
        Vector3 cardinal = SharedLibrary.CardinalDirection(vector);
        return cardinal * distance;
    }

    private abstract class LocationBasedFunctions
    {
        public LineDrawer parent;
        public LocationBasedFunctions(LineDrawer parent)
        {
            this.parent = parent;
        }
        public abstract void _SetInstances(Vector3 begin, Vector3 end);
        public abstract Quaternion _GetDirection(Vector3 begin, Vector3 end);
    }

    private class SameLocation : LocationBasedFunctions
    {
        public SameLocation(LineDrawer parent) : base(parent)
        {
        }

        public override Quaternion _GetDirection(Vector3 begin, Vector3 end)
        {
            var result = new Quaternion();
            var cardinal = SharedLibrary.CardinalDirection(parent.inTilePositon);
            result.SetLookRotation(cardinal);
            return result;
        }

        public override void _SetInstances(Vector3 begin, Vector3 end)
        {
            parent._ghosts = new Vector3[1];
            parent._ghosts[0] = begin;
            return;
        }
    }

    private class DifferentLocation : LocationBasedFunctions
    {
        public DifferentLocation(LineDrawer parent) : base(parent)
        {
        }

        public override Quaternion _GetDirection(Vector3 begin, Vector3 end)
        {
            var result = new Quaternion();
            result.SetLookRotation(SharedLibrary.CardinalDirection(parent.localEnd));
            return result;
        }

        public override void _SetInstances(Vector3 begin, Vector3 end)
        {
            int dist = parent.distanceTile + 1;
            parent._ghosts = new Vector3[dist];

            for (int tile = 0; tile < dist; tile++)
            {
                float tilePosF = (float)tile / (float)parent.distanceTile;
                Vector3 tilePos = Vector3.Lerp(begin, end, tilePosF);
                parent._ghosts[tile] = tilePos;


            }
        }
    }

    [System.Serializable]
    public struct LineData
    {
        public Vector3[] instances;
        public Quaternion direction;
    }
}

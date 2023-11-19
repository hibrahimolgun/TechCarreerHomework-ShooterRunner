using JetBrains.Annotations;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private BreakableObject _wallPrefab;
    [SerializeField] private GameObject _ground;
    [SerializeField] private Transform _wallParent;
    [SerializeField] private float seperation;
    [SerializeField] private int _wallNumber;
    [SerializeField] private SOfloat sectionLength;

    private void Start()
    {
        SpawnWall();
    }

    private void SpawnWall()
    {
        //divide the ground into 10 sections
        sectionLength.value = _ground.transform.lossyScale.x*10 / _wallNumber;

        //spawn 3 walls in each section
        for (int i = 0; i < _wallNumber-1; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var position = new Vector3((i+1) * sectionLength.value, 0, (j-1)*seperation);
                BreakableObject wall = Instantiate(_wallPrefab, position, Quaternion.identity, _wallParent);
                wall.SetIndex(i);
            }
        }


    }

}

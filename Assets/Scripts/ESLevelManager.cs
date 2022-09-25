using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESLevelManager : MoreMountains.TopDownEngine.LevelManager
{
    protected override void Awake() {
        base.Awake();
        _collider = this.GetComponent<Collider>();
        _initialSpawnPointPosition = (InitialSpawnPoint == null) ? Vector3.zero : InitialSpawnPoint.transform.position;
        InstantiatePlayableCharacters();
    }
}

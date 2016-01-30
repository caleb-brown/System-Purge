using UnityEngine;
using System.Collections;

public interface state {
    void Execute();
    void Enter(Enemy enemy);
    void Exit();
    void onTriggerEnter(Collider2D other);
}

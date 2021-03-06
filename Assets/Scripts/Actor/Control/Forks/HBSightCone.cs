﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Forks/Hummingbird/HBSightCone")]
public class HBSightCone : Fork
{
	public LayerMask targetMask;
	public LayerMask obstMask;

	public override bool check (Controller c)
	{
		Hummingbird bird = State.cast<Hummingbird> (c);

		//FIXME remove hard-coded enemy target of player
		RaycastHit2D[] hits;
		hits = Physics2D.CircleCastAll (bird.transform.position, bird.getSightRange (), Vector2.zero, 0f, targetMask.value);
		for (int i = 0; i < hits.Length; i++)
		{
			Entity e = hits [i].collider.GetComponent<Entity> ();
			if (e != null && e.getFaction () == Entity.Faction.player)
			{
				Vector3 dir = hits [i].collider.transform.position - bird.transform.position;
				if (Vector3.Angle (dir, bird.transform.up) < bird.getFOV () / 2f)
				{
					RaycastHit2D wallCheck;
					float dist = Vector2.Distance (bird.transform.position, hits [i].collider.transform.position);
					wallCheck = Physics2D.Raycast (bird.transform.position, dir, dist, obstMask.value);
					if (wallCheck.collider == null)
					{
						bird.setPursuitTarget (hits [i].collider.transform);
						return true;
					}
				}
			}
		}

		return false;
	}
}

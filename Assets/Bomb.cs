using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	public Rotator rotator;

	public void StartBomb()
	{
		rotator.enabled = true;
	}

	public bool PieceWillGoInHole(GameObject piece, GameObject hole, float secLeft)
	{
		if (piece == null) return false;
		var bomb = GetChildWithName("BombBody");
		if (bomb == null) return false;

		float bombHeight = bomb.GetComponent<SpriteRenderer>().bounds.size.y;
		float leftOffset = piece.tag == "Triangle" ? 0.18f : -0.05f;
		float rightOffset = piece.tag == "Triangle" ? 0.475f : 0.42f;
		Vector3 holeLeft = hole.transform.position + (-hole.transform.right * (leftOffset + 0.002f));
		Vector3 holeRight = hole.transform.position + (hole.transform.right * (rightOffset + 0.002f));
		float distFromMagnetToBombCenter = Vector3.Distance(hole.GetComponent<BombHole>().GetMagnet().transform.position, this.transform.position);
		float distFromMagnetToBombEdge = bombHeight / 2 - distFromMagnetToBombCenter;
		float distFromPieceToHole = Vector3.Distance(piece.transform.position, this.transform.position) - bombHeight / 2 + distFromMagnetToBombEdge;
		float timeToImpact = distFromPieceToHole / piece.GetComponent<Rigidbody2D>().velocity.magnitude;
		if (timeToImpact > secLeft) return false;

		Vector3 rotation = new Vector3(0, 0, -this.rotator.Speed * timeToImpact);
		Vector3 futureHoleLeft = RotatePointAroundPivot(holeLeft, transform.position, rotation);
		Vector3 futureHoleRight = RotatePointAroundPivot(holeRight, transform.position, rotation);
		Vector3 pieceDest = piece.GetComponent<Rigidbody2D>().velocity.normalized * distFromPieceToHole * 1.01f;
		pieceDest += piece.transform.position;
		bool willGoIn = AreLinesIntersecting(piece.transform.position, pieceDest, futureHoleLeft, futureHoleRight, true);
		return willGoIn;
	}

	private Vector3 toVector3(Vector2 v2, float z = 0)
	{
		return new Vector3(v2.x, v2.y, z);
	}

	private GameObject GetChildWithName(string name)
	{
		Transform trans = this.transform;
		Transform childTrans = trans.Find(name);
		if (childTrans != null)
		{
			return childTrans.gameObject;
		}
		else
		{
			return null;
		}
	}

	private bool AreLinesIntersecting(Vector2 l1_p1, Vector2 l1_p2, Vector2 l2_p1, Vector2 l2_p2, bool shouldIncludeEndPoints)
	{
		//To avoid floating point precision issues we can add a small value
		float epsilon = 0.00001f;

		bool isIntersecting = false;

		float denominator = (l2_p2.y - l2_p1.y) * (l1_p2.x - l1_p1.x) - (l2_p2.x - l2_p1.x) * (l1_p2.y - l1_p1.y);

		//Make sure the denominator is > 0, if not the lines are parallel
		if (denominator != 0f)
		{
			float u_a = ((l2_p2.x - l2_p1.x) * (l1_p1.y - l2_p1.y) - (l2_p2.y - l2_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;
			float u_b = ((l1_p2.x - l1_p1.x) * (l1_p1.y - l2_p1.y) - (l1_p2.y - l1_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;

			//Are the line segments intersecting if the end points are the same
			if (shouldIncludeEndPoints)
			{
				//Is intersecting if u_a and u_b are between 0 and 1 or exactly 0 or 1
				if (u_a >= 0f + epsilon && u_a <= 1f - epsilon && u_b >= 0f + epsilon && u_b <= 1f - epsilon)
				{
					isIntersecting = true;
				}
			}
			else
			{
				//Is intersecting if u_a and u_b are between 0 and 1
				if (u_a > 0f + epsilon && u_a < 1f - epsilon && u_b > 0f + epsilon && u_b < 1f - epsilon)
				{
					isIntersecting = true;
				}
			}
		}

		return isIntersecting;
	}

	public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
	{
		if (angles.z == double.NegativeInfinity) return point;
		return Quaternion.Euler(angles) * (point - pivot) + pivot;
	}
}

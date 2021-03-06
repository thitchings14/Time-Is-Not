﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RegisteredObject))]
public class SimpleSecurityDoor : MonoBehaviour, IActivatable, ISavable 
{
	[Tooltip("Shows if the door is open or not.")]
	[SerializeField]
	private bool _isOpen = false;

	//the door's sprite
	private SpriteRenderer _sprite;

	//collider of the door
	private Collider2D _collider;

	// Use this for initialization
	void Start () 
	{
		_collider = gameObject.GetComponent<Collider2D> ();
		_sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		#if UNITY_EDITOR
		_sprite = gameObject.GetComponent<SpriteRenderer> ();
		_collider = gameObject.GetComponent<Collider2D>(); 
		if(_isOpen)
		{
			_sprite.enabled = false; 
			_collider.enabled = false; 
		}
		if(!_isOpen)
		{
			_sprite.enabled = true; 
			_collider.enabled = true; 
		}
		#endif
	}

	/// <summary>
	/// Toggle the object's state.
	/// </summary>
	public bool onActivate()
	{
		_isOpen = !_isOpen; 
		return _isOpen;
	}

	/// <summary>
	/// Set the object's state.
	/// </summary>
	public bool onActivate (bool state)
	{
		_isOpen = state; 
		return _isOpen; 
	}

	void Open()
	{
		_isOpen = true;
		_sprite.enabled = false; 
		_collider.enabled = false;
	}

	void Close()
	{
		_isOpen = false;
		_sprite.enabled = true; 
		_collider.enabled = true;
	}

	//****Savable Object Functions****

	/// <summary>
	/// Saves the data into a seed.
	/// </summary>
	/// <returns>The seed.</returns>
	public SeedBase saveData()
	{
		Seed seed = new Seed (gameObject);

		seed.isOpen = _isOpen;

		return seed;
	}

	/// <summary>
	/// Loads the data from a seed.
	/// </summary>
	/// <returns>The seed.</returns>
	public void loadData(SeedBase s)
	{
		if (s == null)
			return;

		Seed seed = (Seed)s;

		s.defaultLoad (gameObject);

		if (seed.isOpen)
			Open ();
		else
			Close ();
	}

	/// <summary>
	/// Checks if the object should be able to be reset.
	/// </summary>
	/// <returns><c>true</c>, if it should ignore it, <c>false</c> otherwise.</returns>
	public bool shouldIgnoreReset() 
	{ 
		return false; 
	}

	/// <summary>
	/// The seed contains all required savable information for the object.
	/// </summary>
	public class Seed : SeedBase
	{
		//is the door open?
		public bool isOpen;

		public Seed(GameObject subject) : base(subject) {}

	}
}

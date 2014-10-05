/*
 *	Script by John Drake for CMU class 15-466 Fall 2011
 *  Updated for Fall 2013 by Evan Shimizu
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TrafficControl : MonoBehaviour
{
	public float minimumCarSpacing = 0.0f;	//should be at least length of one car, i think
	
	private float minimumCarSpacingSquared = 0.0f;
	
	public float randomStartTimePerturbationInSeconds = 0.0f;	//randomly delay some cars, to make the lanes all look different.
	
	private System.Random rnd = null;
	
	public float carSpeed = 5.0f;
	
	private List<Car> _carsCurrentlyInWorld = null;
	//make it read-only, but not readonly :3
	public List<Car> carsCurrentlyInWorld
	{
		get { return this._carsCurrentlyInWorld; }
	}
	
	public class Car
	{
		public readonly GameObject gameObject = null;
		public readonly List<GameObject> wheels = new List<GameObject>();
		public readonly Transform transform = null;
		
		public readonly TrafficLane laneOn = null;
		
		//public readonly GameObject parentGameObject = null;
		//public readonly GameObject aabb = null;
		
		public Car(GameObject _gameObject, GameObject wheel1, GameObject wheel2, GameObject wheel3, GameObject wheel4, TrafficLane _lane)
		{
			this.gameObject = _gameObject;
			transform = this.gameObject.transform;
			this.wheels.Add(wheel1);
			this.wheels.Add(wheel2);
			this.wheels.Add(wheel3);
			this.wheels.Add(wheel4);
			this.laneOn = _lane;
			//this.parentGameObject = this.gameObject.transform.parent.gameObject;
			//this.aabb = this.parentGameObject.transform.FindChild("AABB Obstacle (scripted to stick to the car)").gameObject;
		}
	}
	
	public class TrafficLane
	{
		public readonly GameObject start = null;
		public readonly GameObject end = null;
		public readonly Vector3 startpos = Vector3.zero;
		public readonly Vector3 endpos = Vector3.zero;
		
		private List<Car> _onroad = new List<Car>();
		private List<Car> _offroad = new List<Car>();
		
		public List<Car> onroad
		{
			get { return this._onroad; }
		}
		public List<Car> offroad
		{
			get { return this._offroad; }
		}
		
		public DateTime timeToRezNextCar = DateTime.MinValue;
		
		public readonly Vector3 directionOfTravel = Vector3.zero;
		
		public TrafficLane(GameObject laneobject)
		{
			this.start = laneobject.transform.FindChild("Start").gameObject;
			this.end = laneobject.transform.FindChild("End").gameObject;
			this.startpos = this.start.transform.position;
			this.endpos = this.end.transform.position;
			
			//Debug.Log("Constructing a TrafficLane");
			
			//gets the children transforms of the trafficlane
			foreach (Transform child in laneobject.transform.FindChild("Vehicles"))
			{
				//if (child == laneobject.transform)
					//Debug.Log("match");
				//Debug.Log("child found");
				GameObject car_go = child.gameObject;//.FindChild("catamount").gameObject;
				GameObject catamount = car_go.transform.FindChild("catamount").gameObject;
				Car addme = new Car(car_go,
				                    catamount.transform.FindChild("WheelFL").FindChild("DiscBrakeFL").gameObject,
				                    catamount.transform.FindChild("WheelFR").FindChild("DiscBrakeFR").gameObject,
				                    catamount.transform.FindChild("WheelRR").FindChild("DiscBrakeRR").gameObject,
				                    catamount.transform.FindChild("WheelRL").FindChild("DiscBrakeRL").gameObject,
				                    this);
				offroad.Add(addme);
			}//end loop over children of Vehicles in this lane (the cars themselves)
			
			this.directionOfTravel = this.endpos - this.startpos;
			this.directionOfTravel.Normalize();
		}//end constructor
	}
	
	private List<TrafficLane> _lanes = new List<TrafficLane>();
	public List<TrafficLane> lanes		//hmm.. i don't think this is actually protecting the list itself but whateva
	{
		get { return this._lanes; }
	}
	
	// Use this for initialization
	void Start ()
	{
		this.minimumCarSpacingSquared = this.minimumCarSpacing * this.minimumCarSpacing;
		
		this.rnd = new System.Random();
		
		Debug.Log("Finding traffic lanes.");
		this._lanes = null;
		this._lanes = new List<TrafficLane>();
		//find all lanes
		foreach (GameObject lane in GameObject.FindGameObjectsWithTag("TrafficLane"))
		{
			lanes.Add(new TrafficLane(lane));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//clear out list of cars on the road last frame
		this._carsCurrentlyInWorld = null;
		this._carsCurrentlyInWorld = new List<Car>();
		
		foreach (TrafficLane lane in this.lanes)
		//TrafficLane lane = this.lanes[0];
		{
			//bool scheduled = false;
			if (lane.offroad.Count > 0)
			{
				//Debug.Log("there are extra cars to rez");
				if (lane.timeToRezNextCar.Equals(DateTime.MinValue))
				{
					//Debug.Log("No car has been scheduled to rez.");
					
					//check to see if there's space
					//if so, set lane.timeToRezNextCar
					if (lane.onroad.Count > 0)
					{
						//Debug.Log("Spacing: " + this.minimumCarSpacingSquared + " " + (lane.onroad[lane.onroad.Count-1].transform.position - lane.startpos).sqrMagnitude);
						//the last car rezzed is at the end of the list
					    if ((lane.onroad[lane.onroad.Count-1].transform.position - lane.startpos).sqrMagnitude > this.minimumCarSpacingSquared)
						{
							//Debug.Log("Scheduling min spacing");
							//scheduled = true;
							//Debug.Log("minimum spacing rez");
							lane.timeToRezNextCar = DateTime.Now + TimeSpan.FromSeconds(rnd.NextDouble() * this.randomStartTimePerturbationInSeconds);
							//if (lane.timeToRezNextCar > DateTime.Now)
								//Debug.Log("WHAT that makes absolutely no sense");
						}
					}
					else
					{
						//scheduled = true;
						//Debug.Log("Scheduling");
						lane.timeToRezNextCar = DateTime.Now + TimeSpan.FromSeconds(rnd.NextDouble() * this.randomStartTimePerturbationInSeconds);
					}
				}//end check that there is a car waiting to rez and a rez time has not yet been set
				
				//if (lane.timeToRezNextCar != DateTime.MinValue)
					//Debug.Log("Time: " + (lane.timeToRezNextCar - DateTime.Now));
				
				//now see if we can rez one now
				if ((lane.timeToRezNextCar != DateTime.MinValue) && (DateTime.Now > lane.timeToRezNextCar))
				{
					//rez the car
					//Debug.Log("rezzing a car" + this.rnd.NextDouble().ToString());
					
					//reset car position
					lane.offroad[0].transform.position = lane.startpos;
					
					//move from offroad list to onroad list
					lane.onroad.Add(lane.offroad[0]);
					lane.offroad.RemoveAt(0);
					
					
					//reset rez time variable
					lane.timeToRezNextCar = DateTime.MinValue;
				}//end check if it's time to rez a car
				//else
					//if (scheduled)
						//Debug.Log("missed opportunity to rez... why?");
			}//end check that there is a car waiting to rez
			
			//make cars move
			//move finished cars off road
			//make wheels spin?
			int i = 0;
			for (i = 0; i < lane.onroad.Count; i++)
			{
				Car car = lane.onroad[i];
				
				//populate list of cars currently in world
				this.carsCurrentlyInWorld.Add(car);
				
				Vector3 direction = lane.endpos - lane.startpos;
				direction.Normalize();
				
				float distance_to_end = (lane.endpos - car.transform.position).magnitude;
				Vector3 desired_position = carSpeed * Time.deltaTime * direction + car.transform.position;
				float distance_to_desired_position = (lane.endpos - desired_position).magnitude;
				
				//check that the car isn't trying to go too far
				if (distance_to_desired_position < distance_to_end)
					//not trying to go too far
					car.transform.position = desired_position;
				else
				{
					
					car.transform.position = lane.endpos;
					//Debug.Log("at end of lane");
				}
				
				//if they're at the end, remove them
				//assumes cars all go the same speed
				if ((car.transform.position - lane.endpos).sqrMagnitude < 0.1f)
				{
					//Debug.Log("being removed from road");
					//remove from onroad (will be at 0)
					lane.offroad.Add(lane.onroad[0]);
					lane.onroad.RemoveAt(0);
					i--;
				}
				
				//rotate wheels
				foreach(GameObject wheel in car.wheels)
				{
					wheel.transform.localRotation = Quaternion.AngleAxis(Time.time * 1000.0f, Vector3.right);
				}
			}//end loop over cars
		}//end loop over lanes
	}//end update
}//end script class

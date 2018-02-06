using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts
{
    class Car
    {
        //Main class variables:
        //private variables:
        private float speedConstant; //The current car's main speed constant that it can accelerate at, this will be used to add force to the car
        private float jumpHeightConstant; //The constant for how much force can be added to the jump
        private float gravityConstant; //The gravity constant will be set for the map that is being used

        private Rigidbody carRB; // This will be changed once grey box is given 
        private GameObject carGO; // same as rigidbody, they are temproary so that the class can show movement

        //public variables:
        public float gravityPowerup; //This will be used for the powers to change the gravity for the cars
        public float coinCollectionStore; //This is temproary until the coin class is created

        //checkpoint variables will more than likely be replaced by their own class
        public int currentCheckpoint; //The checkpoint that the player went past last
        public int nextCheckpoint; //The checkpoint that the player will go through next

        //boolean variables:
        public bool deathCheck; //This is to check if the car as been killed

        //Methods:
        //setting the private variables by instanitating the class, carRB and carGO will be replaced later
        public Car(float _speedConstant, float _jumpHeightConstant, float _gravityConstant, Rigidbody _carRB, GameObject _carGO)
        {
            speedConstant = _speedConstant;
            jumpHeightConstant = _jumpHeightConstant;
            gravityConstant = _gravityConstant;
            carRB = _carRB;
            carGO = _carGO;
        }

        //These are placeholder methods to show what the class will more than likely need
        //Acceleration of the car to begin moving forward
        public void Acceleration()
        {

        }

        //For the car to move backwards if stuck
        public void Reverse()
        {

        }

        //Allow the car turning, this might be put with acceleration
        public void Turning()
        {

        }

        //For the speedboost powerup, will add a multiplyer to the constant
        public void SpeedBoost()
        {

        }

        //So that the car can collect the coins and display them in the corner
        public void CoinCollection()
        {

        }

        //To store the coins into a database or other storage system
        public void CoinStoring()
        {

        }

        //Gravity increase powerup, allow the car to jump higher
        public void GravityIncrease()
        {

        }

        //Decrease the gravity and cause the car to move slower and jump not as high
        public void GravityDecrease()
        {

        }

        //Kills the car when the car is hit
        public void DeathOfCar()
        {

        }

        //WIll respawn the car at the last checkpoint
        public void Respawn()
        {

        }

        //Updates the checkpoints to the next one on the map
        public void UpdateCheckpoint()
        {

        }



    }
}

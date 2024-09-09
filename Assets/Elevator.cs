using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    // I chose to follow the elevator pseudo code i created in the first lesson
    // it was easier to think of what methods are required because of the plan.
    private int currentFloor = 0;
    private int targetFloor;
    private List<int> selectedFloors = new List<int>();
    private int capacity = 10;
    private bool isMoving = false;
    private bool isLocked = false;
    private bool doorOpen = false;
    private bool isFanOn = false;
    private bool isAlarmOn = false;
    private int currentLoad = 5;  
    private int buildingFloorCount = 10;
    // this obviously wont be saved in a place like this. for simplification,
    // i chose to keep it here
    private int elevatorPass = 123456 ;
    
// check if now is Shabat 
//(i checked for Saturday, not Shabat enter and ending times.)
    public bool IsShabatNow()
    {
        // get the current local time
        DateTime currentTime = DateTime.Now;
        
        // check if today is Saturday (Shabbat)
        bool isShabat = currentTime.DayOfWeek == DayOfWeek.Saturday;
        
        Debug.Log("Is Shabbat now: " + isShabat);
        return isShabat;
    }

    // when a floor is selected its added to the selected floor list.
     public void SelectFloor(int floor)
    {
        if (!isLocked) {
        selectedFloors.Add(floor);
        Debug.Log("Floor " + floor + " selected.");
        }
    }

    public void GoToNextFloor()
    {
        // this is to prevent the elevator moving to next floor is the list is empty.
        // and to prevent is moving when locked
        if (selectedFloors.Count > 0 && !isLocked)
        {
            // add the first floor in the list as the next target floor
            targetFloor = selectedFloors[0];
            // remove that floor from the list
            selectedFloors.RemoveAt(0);
            // start the method that moves the elevator
            MoveToFloor(targetFloor);
        }
    }

    // methid that opens the doors. currently sets doorOpen to true and logs it.
    public void OpenDoor()
    {
        doorOpen = true;
        Debug.Log("Door is open.");
    }
    // same as open door but for closing.
        public void CloseDoor()
    {
        doorOpen = false;
        Debug.Log("Door is closed.");
    }


    // this method is changing the current floor by a specified number of floors
    // depending on the provided direction
    public void MoveElevator(int howManyFloors, string direction) 
    { 
        if (isLocked) {
            Debug.Log("the elevator is locked and cannot be moved!");
            return ;
        }
        switch (direction)
        {
            case "up":
              currentFloor += howManyFloors;
                Debug.Log("Moved up " + howManyFloors + " floors. Current floor: " + currentFloor);
                break;
            case "down":
                currentFloor -= howManyFloors;
                Debug.Log("Moved down " + howManyFloors + " floors. Current floor: " + currentFloor);
                break;
            default:
                Debug.Log("The elevator is not moving now.");
                break;
        }
    }

    // method to stop the elevator at the target floor
    public void Stop(int floor)
    {
        if (currentFloor == floor)
        {
            isMoving = false;
            Debug.Log("Elevator stopped at floor " + floor);
        }
    }

    // this method expects a floor number and moves up or down 
    // acoording to the current location.
     private void MoveToFloor(int floor)
    {
        while (currentFloor != floor)
        { 
            isMoving = true;
            if (currentFloor < floor)
            {
                MoveElevator(1, "up");
            }
            else
            {
                MoveElevator(1, "down");
            }
        }
        Stop(floor);
    }

     // emergency stop method, sets ismoving to false, logs this and open the doors.
    public void EmergencyStop()
    {
        isMoving = false;
        Debug.Log("Emergency stop activated!");
        OpenDoor();
    }

    // sets moving to false, is locked to true, and opens the doors.
    public void LockElevator()
    {
        isMoving = false;
        isLocked = true;
        Debug.Log("Elevator locked!");
        OpenDoor();
    }

    public void unlockElevator(int elevatorKey)
    {
        if (elevatorKey == elevatorPass) {
            isLocked = false;
        }
    }

    // toggle the fan status
    public void ToggleFan()
    {
        isFanOn = !isFanOn;
          Debug.Log("Fan is " + (isFanOn ? "on." : "off."));
    }

    // toggle alarm
    public void ToggleAlarm()
    {
        isAlarmOn = !isAlarmOn;
        Debug.Log("Alarm is " + (isAlarmOn ? "on." : "off."));
    }

    // unselect a floor by checking if its in the lsit and than removing it
    public void UnselectFloor(int floor)
    {
        if (selectedFloors.Contains(floor))
        {
            selectedFloors.Remove(floor);
            Debug.Log("Floor " + floor + " unselected.");
        }
    }

    // method to simulate Shabbat behavior (stopping at every floor)
    public void DoShabatBehaviour()
    {
        for (int i = 0; i <= buildingFloorCount; i++) 
        {
            currentFloor = i;
            Debug.Log("Stopping at floor " + i + " for Shabbat.");
            MoveElevator(1, "up");
            OpenDoor();
            CloseDoor();
        }
    }

    
    void Start()
    {
        bool isShabat = IsShabatNow();

        if (isShabat)
        {
            DoShabatBehaviour();
        }
        else
        {
        //instead of this:
            SelectFloor(5);
            GoToNextFloor();
            MoveElevator(3, "up");
            MoveElevator(2, "down");
            OpenDoor();
            CloseDoor();
            EmergencyStop();
            LockElevator();
            ToggleFan();
            ToggleFan();
            ToggleAlarm();
            unlockElevator(123456); //this should be user input
            UnselectFloor(5);
            GoToNextFloor();

            // to much code in Start(). you should migrate that into another method to ease readability
            // do this:
            // RegularBehaviour();
            // its easy to understand that there are two types of behviours,l shabat or regular. 
            
        }
/*
        void RegularBehaviour(){
                    SelectFloor(5);
            GoToNextFloor();
            MoveElevator(3, "up");
            MoveElevator(2, "down");
            OpenDoor();
            CloseDoor();
            EmergencyStop();
            LockElevator();
            ToggleFan();
            ToggleFan();
            ToggleAlarm();
            unlockElevator(123456); //this should be user input
            UnselectFloor(5);
            GoToNextFloor();
        }
        */
    }

}


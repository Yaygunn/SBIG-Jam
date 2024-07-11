using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities.Singleton;

namespace Manager.Task
{
    public class TaskManager : Singleton<TaskManager>
    {
        [SerializeField] private GameObject taskTextUIContainer;
        [SerializeField] private TextMeshProUGUI taskTextUI;
        private Queue<string> tasks = new Queue<string>();
        private string currentTask;
        private bool isTaskInProgress = false;

        private void Start()
        {
            tasks.Enqueue("Pick 1 plant from the garden");
            tasks.Enqueue("Add plant to cauldron");
            tasks.Enqueue("Pick 1 plant from the garden");
            tasks.Enqueue("Add plant to cauldron");
            tasks.Enqueue("Pick 1 plant from the garden");
            tasks.Enqueue("Add plant to cauldron");
            tasks.Enqueue("Wait for cauldron to finish cooking");
            tasks.Enqueue("Reload weapon");
            tasks.Enqueue("Defeat incoming wave of enemies");
            // Not sure we need to show them this again
            tasks.Enqueue("Pick and throw 3 plants into the cauldron");
            tasks.Enqueue("Defeat incoming wave of enemies");

            HideTasks();
            StartTasks();
        }

        public void StartTasks()
        {
            ShowNextTask();
        }

        private void ShowNextTask()
        {
            if (tasks.Count > 0)
            {
                currentTask = tasks.Dequeue();
                taskTextUI.text = currentTask;
                isTaskInProgress = true;

                ShowTasks();
            }
            else
            {
                HideTasks();
            }
        }
        
        public bool IsCurrentTask(string task)
        {
            return currentTask == task;
        }
        
        public void CompleteTask(string completedTask)
        {
            if (IsCurrentTask(completedTask))
            {
                isTaskInProgress = false;
                ShowNextTask();
            }
        }
        
        private void HideTasks()
        {
            taskTextUIContainer.SetActive(false);
            taskTextUI.text = "";
        }

        private void ShowTasks()
        {
            taskTextUIContainer.SetActive(true);
        }
    }   
}

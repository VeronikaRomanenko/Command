using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Robot robot = new Robot();
            Invoker invoker = new Invoker();
            invoker.SetCommand(new MoveForvardCommand(robot, 3));
            invoker.SetCommand(new RotateCommand(robot, 25));
            invoker.SetCommand(new HandUpCommand(robot, 10));
            invoker.Run();
            invoker.Cancel(3);
        }
    }
    class Robot
    {
        public void MoveForward(int step)
        {
            if (step > 0)
            {
                Console.WriteLine($"Робот сделал {step} шагов вперед");
            }
            else
            {
                Console.WriteLine($"Робот сделал {-1 * step} шагов назад");
            }
        }
        public void Rotate(int dogree)
        {
            if (dogree > 0)
            {
                Console.WriteLine($"Робот повернул налево на {dogree} градусов");
            }
            else
            {
                Console.WriteLine($"Робот повернул направо на {-1 * dogree} градусов");
            }
        }
        public void HandUp(int cm)
        {
            if (cm > 0)
            {
                Console.WriteLine($"Робот поднял руку на {cm} сантиметров");
            }
            else
            {
                Console.WriteLine($"Робот опустил руку на {-1 * cm} сантиметров");
            }
        }
    }
    interface ICommand
    {
        void Execute();
        void Undo();
    }
    class MoveForvardCommand : ICommand
    {
        private Robot receiver;
        private int step;
        public MoveForvardCommand(Robot robot, int step)
        {
            receiver = robot;
            this.step = step;
        }
        public void Execute()
        {
            receiver.MoveForward(step);
        }
        public void Undo()
        {
            receiver.MoveForward(-1 * step);
        }
    }
    class RotateCommand : ICommand
    {
        private Robot receiver;
        private int dogree;
        public RotateCommand(Robot robot, int dogree)
        {
            receiver = robot;
            this.dogree = dogree;
        }
        public void Execute()
        {
            receiver.Rotate(dogree);
        }
        public void Undo()
        {
            receiver.Rotate(-1 * dogree);
        }
    }
    class HandUpCommand : ICommand
    {
        private Robot receiver;
        private int cm;
        public HandUpCommand(Robot robot, int cm)
        {
            receiver = robot;
            this.cm = cm;
        }
        public void Execute()
        {
            receiver.HandUp(cm);
        }
        public void Undo()
        {
            receiver.HandUp(-1 * cm);
        }
    }
    class Invoker
    {
        private Queue<ICommand> commandQueue = new Queue<ICommand>();
        private Stack<ICommand> commandStack = new Stack<ICommand>();
        public void SetCommand(ICommand command)
        {
            commandQueue.Enqueue(command);
        }
        public void Run()
        {
            do
            {
                ICommand command = commandQueue.Dequeue();
                command.Execute();
                commandStack.Push(command);
            } while (commandQueue.Count > 0);
        }
        public void Cancel(int number)
        {
            for (int i = 0; i < number; i++)
            {
                if (commandStack.Count > 0)
                {
                    commandStack.Pop().Undo();
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using MarsRover.Models.Enums;
using MarsRover.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.Models
{    
    public class Rover : IVehicle
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Cardinal Cardinal { get; private set; }
        
        public IPlateau Plateau { get; }

        public Rover(IServiceProvider serviceProvider)
        {
           Plateau = serviceProvider.GetService<IPlateau>();
        }

        public void Move(Movement movement)
        {
            switch (movement)
            {
                case Movement.M:
                    MoveForward();
                    break;
                case Movement.L:
                    TurnLeft();
                    break;
                case Movement.R:
                    TurnRight();
                    break;
                default:
                    throw new Exception("Unidentified command.");
            }
        }

        private void MoveForward()
        {
            switch (Cardinal)
            {
                case Cardinal.N:
                    if (Y + 1 < Plateau.Size.Height) Y +=1;
                    break;

                case Cardinal.E:
                    if (X + 1 < Plateau.Size.Width) X += 1;
                    break;

                case Cardinal.S:
                    if (Y - 1 >= 0) Y-= 1;
                    break;

                case Cardinal.W:
                    if (X - 1 >= 0) X-= 1;
                    break;

                default:
                    throw new Exception($"Unidentified direction({Cardinal}).");
            }
        }

        private void TurnLeft()
        {
            switch (Cardinal)
            {
                case Cardinal.N:
                    Cardinal = Cardinal.W;
                    break;

                case Cardinal.W:
                    Cardinal = Cardinal.S;
                    break;

                case Cardinal.S:
                    Cardinal = Cardinal.E;
                    break;

                case Cardinal.E:
                    Cardinal = Cardinal.N;
                    break;
                default:
                    throw new Exception("Unidentified cardinal.");
            }
        }

        private void TurnRight()
        {
            switch (Cardinal)
            {
                case Cardinal.N:
                    Cardinal = Cardinal.E;
                    break;

                case Cardinal.E:
                    Cardinal = Cardinal.S;
                    break;

                case Cardinal.S:
                    Cardinal = Cardinal.W;
                    break;

                case Cardinal.W:
                    Cardinal = Cardinal.N;
                    break;
                default:
                    throw new Exception("Unidentified cardinal.");
            }
        }

        public void Deploy(int x, int y, Cardinal cardinal)
        {
            if (!ValidatePosition(x, y))
            {
                throw new Exception("Given location is outside of bounds.");
            }
            X = x;
            Y = y;
            Cardinal = cardinal;
        }

        public string ReportPosition()
        {
            return $"{X} {Y} {Cardinal:G}";
        }

        private bool ValidatePosition(int x, int y)
        {
            if (Plateau.Size == null)
            {
                throw new Exception("Plateau surface size not defined yet.");
                
            }
            return x >= 0 && x < Plateau.Size.Width &&
                   y >= 0 && y < Plateau.Size.Height;
        }
    }
}

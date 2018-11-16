using Beskyddare.Debug;
using Beskyddare.Entities;
using Beskyddare.Logic;
using Beskyddare.Utility;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.AI
{
    public class ChaseAI : BaseAI, DebugPainter
    {
        public enum ChaseAIState
        {
            Idle,
            Wandering,
            Chasing
        }

        public ChaseAIState state = ChaseAIState.Idle;
        public float timeAccumulator;
        public TargetInfo target;
        //Vector2 debugHitLocation = new Vector2();

        public override void TickAI(float delta)
        {
            Player player = Player;
            if (player == null)
            {
                return;
            }

            Movable locomotion = Locomotion;
            bool move = false;
            switch(state)
            {
                case ChaseAIState.Idle:
                    {
                        //Wait a while until deciding what to do next.
                        TickDownTime(delta);

                        if (timeAccumulator <= 0)
                        {
                            if (locomotion.CanSeeTarget(player, Controller.searchRadius))
                            {
                                target = player;
                                state = ChaseAIState.Chasing;
                                timeAccumulator = Controller.chaseTime;
                            }
                            else
                            {
                                //Random chance of either continue idling or start wandering.
                                if (GenRandom.Bool())
                                {
                                    //Wander.
                                    state = ChaseAIState.Wandering;

                                    //Pick a random spot to wander to.
                                    int randomDirection = GenRandom.Integer(GenDirection.EightWay.Length);
                                    Vector2 randomDirectionVector = GenDirection.EightWay[randomDirection];
                                    Vector2 randomLocationVector = GenMath.OffsetInDirectionVector(locomotion.GlobalPosition, randomDirectionVector, Controller.wanderDistance);
                                    //Vector2 randomLocationVector = locomotion.Position + (randomDirectionVector.Normalized() * Controller.wanderDistance);

                                    bool rayHit = false;
                                    Vector2 rayHitVector = GenPhysics.SimpleRayCast(locomotion, locomotion.GlobalPosition, randomLocationVector, out rayHit);
                                    //debugHitLocation = rayHitVector;
                                    if (!rayHit)
                                    {
                                        target = randomLocationVector;
                                    }
                                    else
                                    {
                                        target = rayHitVector;
                                    }
                                }
                                else
                                {
                                    timeAccumulator = Controller.idleTime;
                                }
                            }
                        }
                    }
                    break;

                case ChaseAIState.Chasing:
                    {
                        //Chase!
                        ChasePlayer(delta);
                        move = true;

                        if (locomotion.CanSeeTarget(player, Controller.searchRadius))
                        {
                            //Reset chase time
                            timeAccumulator = Controller.chaseTime;
                        }
                        else
                        {
                            //Go back to idling after time passed.
                            TickDownTime(delta);

                            if (timeAccumulator <= 0)
                            {
                                SwitchToIdleState();
                            }
                        }
                    }
                    break;

                case ChaseAIState.Wandering:
                    {
                        if(locomotion.hitObstacle)
                        {
                            SwitchToIdleState();
                        }
                        else
                        {
                            //Stop wandering if nearby the wandering location.
                            if (locomotion.GlobalPosition.DistanceTo(target.Location) <= 16f)
                            {
                                SwitchToIdleState();
                            }
                            else
                            {
                                MoveToward(target.Location, Controller.movementSpeed * 0.5f);
                                move = true;
                            }
                        }
                    }
                    break;
            }

            if(!move)
            {
                StopLocomotion();
            }
        }

        public void SwitchToIdleState()
        {
            state = ChaseAIState.Idle;
            timeAccumulator = Controller.idleTime;
        }

        public void TickDownTime(float delta)
        {
            timeAccumulator -= 1f;
        }

        public void MoveToward(Vector2 position, float velocity)
        {
            Movable locomotion = Locomotion;

            Vector2 directionVector = new Vector2();
            if (position.y < locomotion.GlobalPosition.y)
            {
                directionVector.y = -1f;
            }
            else if (position.y > locomotion.GlobalPosition.y)
            {
                directionVector.y = 1f;
            }

            if (position.x < locomotion.GlobalPosition.x)
            {
                directionVector.x = -1f;
            }
            else if (position.x > locomotion.GlobalPosition.x)
            {
                directionVector.x = 1f;
            }

            locomotion.velocity = directionVector.Normalized() * velocity;
        }

        public void ChasePlayer(float delta)
        {
            //Player player = Player;
            //Movable locomotion = Locomotion;
            MoveToward(target.Location, Controller.movementSpeed);
        }

        public void PaintDebug(DebugDrawer drawer)
        {
            if(Settings.Data.debugMode)
            {
                Movable locomotion = Locomotion;

                drawer.DrawCircle(drawer.Position, Controller.searchRadius, new Color(1.0f, 0.0f, 0.0f, 0.2f));
                //drawer.DrawLine(drawer.Position, debugHitLocation.RelativeTo(locomotion.GlobalPosition), new Color(0.0f, 1.0f, 0.0f), 2f, true);
                if(target != null)
                {
                    const float size = 24f;
                    drawer.DrawRect(new Rect2(target.Location.RelativeTo(locomotion.GlobalPosition) - new Vector2(size / 2f, size / 2f), size, size), new Color(0.0f, 1.0f, 0.0f), false);
                }
                
                if (state == ChaseAIState.Wandering)
                {
                    drawer.DrawLine(drawer.Position, target.Location.RelativeTo(locomotion.GlobalPosition), new Color(0.0f, 0.0f, 1.0f), 2f, true);
                    drawer.DrawCircle(target.Location.RelativeTo(locomotion.GlobalPosition), 8f, new Color(0.0f, 0.0f, 1.0f));
                }
                else if (state == ChaseAIState.Chasing)
                {
                    drawer.DrawLine(drawer.Position, target.Location.RelativeTo(locomotion.GlobalPosition), new Color(1.0f, 0.0f, 0.0f), 2f, true);
                    drawer.DrawCircle(target.Location.RelativeTo(locomotion.GlobalPosition), 8f, new Color(1.0f, 0.0f, 0.0f));
                }
            }
        }

        public bool ShouldRedraw(DebugDrawer drawer)
        {
            return true;
        }
    }
}

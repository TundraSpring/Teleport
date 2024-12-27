using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;
using static System.Formats.Asn1.AsnWriter;

public partial class player2 : Area2D
{
    //ScreenSize = GetViewportRect().Size;
    //public Vector2 ScreenSize;
    //public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        //ScreenSize = GetViewportRect().Size;
        //dummy = GetNode<AnimatedSprite2D>("Dummy");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        
    }

    public override void _Input(InputEvent @event)
    {
        //ScreenSize = GetViewportRect().Size;
    }

    private void Archive()
    {
        //Vector2 secondMovement;
        //double degrees = A((desiredPos + body.Position), soul.Position);
        //if (degrees >= 90 && degrees <= 180)
        //{
        //    secondMovement = new Vector2(10, -10);
        //}
        //else if (degrees >= 0 && degrees <= 90)
        //{
        //    secondMovement = new Vector2(-10, -10);
        //}
        //else if (degrees >= -90 && degrees <= 0)
        //{
        //    secondMovement = new Vector2(-10, 10);
        //}
        //else // (degrees >= -180 && degrees <= -90)
        //{
        //    secondMovement = new Vector2(10, 10);
        //}
        //KinematicCollision2D secondCollision = scout.MoveAndCollide(secondMovement);









        //double degrees = RadToDeg(collision.GetAngle());

        //Vector2 secondMovement;
        //if (degrees == 90)
        //{
        //    if (soul.Position.Y < scout.Position.Y)
        //    {
        //        secondMovement = new Vector2(0, 10);
        //        GD.Print("down");
        //        GD.Print(degrees);
        //    }
        //    else
        //    {
        //        secondMovement = new Vector2(0, -10);
        //        GD.Print("up");
        //        GD.Print(degrees);
        //    }
        //}
        //else
        //{
        //    if (soul.Position.X < scout.Position.X)
        //    {
        //        secondMovement = new Vector2(10, 0);
        //        GD.Print("right");
        //        GD.Print(degrees);
        //    }
        //    else
        //    {
        //        secondMovement = new Vector2(-10, 0);
        //        GD.Print("left");
        //        GD.Print(degrees);
        //    }
        //}





        //double degrees = RadToDeg(collision.GetAngle());

        //Vector2 secondMovement;
        //if ((degrees >= 90 && degrees <= 135) && soul.Position.Y > scout.Position.Y)
        //{
        //    GD.Print("up");
        //}
        //else if ((degrees >= 45 && degrees <= 90) && soul.Position.Y < scout.Position.Y)
        //{
        //    GD.Print("down");
        //}
        //else if ((degrees >= 0 && degrees <= 45) && soul.Position.X < scout.Position.X)
        //{
        //    GD.Print("right");
        //}
        //else if ((degrees >= 135 && degrees <= 180) && soul.Position.X < scout.Position.X)
        //{
        //    GD.Print("right");
        //}
        //else if ((degrees >= 0 && degrees <= 45) && soul.Position.X > scout.Position.X)
        //{
        //    GD.Print("left");
        //}
        //else if ((degrees >= 135 && degrees <= 180) && soul.Position.X > scout.Position.X)
        //{
        //    GD.Print("left");
        //}




        //public void SetPlayerSoulPosition()
        //{
        //    Vector2 cursorPos = body.GetLocalMousePosition();

        //    //if distance between cursor and playerBody is greater than 150 (pixels?), then set cursorPos (Not the actual cursor) to be 150 away from playerBody, pointing towards cursor
        //    double cursorDistance = Math.Sqrt(Math.Pow(cursorPos.X - 0, 2) + Math.Pow(cursorPos.Y - 0, 2));
        //    if (cursorDistance > 350)
        //    {
        //        double multiplier = 350 / cursorDistance;
        //        cursorPos = new Vector2((float)(cursorPos.X * multiplier), (float)(cursorPos.Y * multiplier)); //HERE
        //    }
        //    soul.Position = cursorPos + body.Position;
        //    soul.Position = new Vector2(
        //        x: Mathf.Clamp(soul.Position.X, 65, ScreenSize.X - 65),
        //        y: Mathf.Clamp(soul.Position.Y, 48, ScreenSize.Y - 48)
        //    );
        //}

        //public void SetPlayerSoulPosition2(double delta)
        //{


        //    Vector2 cursorPos = body.GetLocalMousePosition();

        //    //if distance between cursor and playerBody is greater than 150 (pixels?), then set cursorPos (Not the actual cursor) to be 150 away from playerBody, pointing towards cursor
        //    double cursorDistance = Math.Sqrt(Math.Pow(cursorPos.X - 0, 2) + Math.Pow(cursorPos.Y - 0, 2));
        //    if (cursorDistance > 350)
        //    {
        //        double multiplier = 350 / cursorDistance;
        //        cursorPos = new Vector2((float)(cursorPos.X * multiplier), (float)(cursorPos.Y * multiplier)); //HERE
        //    }
        //    soul.Position = cursorPos + body.Position;
        //    //playerSoul.Position = new Vector2(
        //    //    x: Mathf.Clamp(playerSoul.Position.X, 65, ScreenSize.X - 65),
        //    //    y: Mathf.Clamp(playerSoul.Position.Y, 48, ScreenSize.Y - 48)
        //    //);

        //    soul.Velocity = cursorPos + body.Position;
        //    soul.MoveAndSlide();
        //}


        //public void SetPlayerSoulPosition3(double delta)
        //{




        //    Vector2 cursorPos = body.GetLocalMousePosition();

        //    //if distance between cursor and playerBody is greater than 150 (pixels?), then set cursorPos (Not the actual cursor) to be 150 away from playerBody, pointing towards cursor
        //    double cursorDistance = Math.Sqrt(Math.Pow(cursorPos.X - 0, 2) + Math.Pow(cursorPos.Y - 0, 2));
        //    if (cursorDistance > 350)
        //    {
        //        double multiplier = 350 / cursorDistance;
        //        cursorPos = new Vector2((float)(cursorPos.X * multiplier), (float)(cursorPos.Y * multiplier)); //HERE
        //    }

        //    soul.Position = cursorPos + body.Position;
        //    //playerSoul.Position = new Vector2(
        //    //    x: Mathf.Clamp(playerSoul.Position.X, 65, ScreenSize.X - 65),
        //    //    y: Mathf.Clamp(playerSoul.Position.Y, 48, ScreenSize.Y - 48)
        //    //);





        //    //playerSoul.Velocity = cursorPos + playerBody.Position;
        //    soul.MoveAndSlide();
        //    //playerSoul.Velocity = Vector2.Zero;

        //    //for (int i = 0; i < playerSoul.GetSlideCollisionCount(); i++)
        //    //{
        //    //KinematicCollision2D collision = playerSoul.GetSlideCollision(i);
        //    //GD.Print("Collided with: ", (collision.GetCollider() as Node).Name);
        //    //}
        //    KinematicCollision2D collision = scout.MoveAndCollide(Vector2.Zero);
        //    if (collision != null)
        //    {
        //        dummy.GlobalPosition = collision.GetPosition();
        //        GD.Print("PlayerTether: ", scout.GlobalPosition);
        //        GD.Print("PlayerSoul:   ", soul.GlobalPosition);
        //        GD.Print("PlayerBody:   ", body.GlobalPosition);
        //        GD.Print("Collision:    ", dummy.GlobalPosition);
        //    }
        //    scout.Position = soul.Position;
        //}

        //public void SetPlayerSoulPosition4(double delta)
        //{
        //    Vector2 cursorPos = GetCursorPos();
        //    tether.Position = cursorPos + body.Position;

        //    //double degrees = CalculateDegreesBetweenPoints(playerTether.GlobalPosition, dummy.GlobalPosition);
        //    //double degrees = dummy.GetAngleTo(playerTether.GlobalPosition);
        //    //GD.Print("degrees:      ", degrees);





        //    KinematicCollision2D collision = tether.MoveAndCollide(Vector2.Zero);
        //    if (collision != null)
        //    {
        //        //there are 27 pixels from center to edge of the circles
        //        dummy.GlobalPosition = collision.GetPosition();
        //        GD.Print("PlayerTether: ", tether.GlobalPosition);
        //        GD.Print("PlayerSoul:   ", soul.GlobalPosition);
        //        GD.Print("PlayerBody:   ", body.GlobalPosition);
        //        GD.Print("Collision:    ", dummy.GlobalPosition);
        //        tether.GlobalPosition = CalculateDegreesBetweenPoints(dummy.GlobalPosition, tether.GlobalPosition);



        //        if (body.GlobalPosition.Y < tether.GlobalPosition.Y && tether.GlobalPosition.Y < dummy.GlobalPosition.Y)
        //        {
        //            GD.Print("Not through");
        //        }
        //        else if (body.GlobalPosition.Y < tether.GlobalPosition.Y && tether.GlobalPosition.Y > dummy.GlobalPosition.Y)
        //        {
        //            GD.Print("Through");
        //        }
        //    }
        //    else
        //    {
        //        soul.Position = cursorPos + body.Position;
        //    }

        //    //playerTether.Position = playerSoul.Position;
        //}



        //public void SetPlayerSoulPosition5(double delta)
        //{
        //    Vector2 desiredPos = GetCursorPos() + body.Position;

        //    double distanceToSoul = Math.Sqrt(Math.Pow(desiredPos.X - soul.Position.X, 2) + Math.Pow(desiredPos.Y - soul.Position.Y, 2));
        //    double degreesToSoul = A(desiredPos, soul.Position) - 90;
        //    tether.RotationDegrees = (float)degreesToSoul;
        //    tether.Scale = new Vector2(tether.Scale.X, (float)(distanceToSoul * 0.0322));

        //    KinematicCollision2D collision = tether.MoveAndCollide(Vector2.Zero);
        //    if (collision != null)
        //    {
        //        GD.Print("true");
        //        soul.GlobalPosition = CalculateDegreesBetweenPoints(collision.GetPosition(), tether.GlobalPosition);
        //    }
        //    else
        //    {
        //        GD.Print("false");
        //        soul.GlobalPosition = GetCursorPos() + body.GlobalPosition;
        //    }
        //    tether.Position = soul.Position;
        //}

        //public void SetPlayerSoulPosition6(double delta)
        //{
        //    Vector2 desiredPos = GetCursorPos() + body.Position;
        //    scout.Position = desiredPos;
        //    SetPlayerTether(desiredPos);

        //    KinematicCollision2D collision = tether.MoveAndCollide(Vector2.Zero);
        //    if (collision != null)
        //    {
        //        soul.GlobalPosition = collision.GetPosition();
        //        soul.MoveAndSlide();
        //    }
        //    else
        //    {

        //    }
        //    tether.Position = soul.Position;
        //    scout.Position = soul.Position;
        //}

        //public void PlayerBodyJumpAndGravity(double delta)
        //{
        //    Vector2 velocity = body.Velocity;

        //    // Add the gravity.
        //    if (!body.IsOnFloor() && velocity.Y <= 5000)
        //        velocity.Y += 100;   // gravity * (float)delta;

        //    // Handle Jump.
        //    if (Input.IsActionJustPressed("ui_accept") && body.IsOnFloor())
        //        velocity.Y += -1500F;

        //    Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        //    if (direction != Vector2.Zero)
        //    {
        //        velocity.X = (direction.X * 10) * Speed;
        //    }
        //    else
        //    {
        //        velocity.X = Mathf.MoveToward(body.Velocity.X, 0, Speed);
        //    }


        //    body.Velocity = velocity;
        //    body.MoveAndSlide();


        //}


        //public void SetPlayerBodySprite(ref Vector2 playerBodyVelocity)
        //{
        //    AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");

        //    if (playerBodyVelocity.Length() > 0)
        //    {
        //        playerBodyVelocity = playerBodyVelocity * Speed;
        //        if (playerBodyVelocity.X != 0)
        //        {
        //            animatedSprite2D.Play();
        //            animatedSprite2D.FlipH = playerBodyVelocity.X < 0;
        //        }
        //        else
        //        {
        //            animatedSprite2D.Stop();
        //        }
        //    }
        //    else
        //    {
        //        animatedSprite2D.Stop();
        //    }


        //}



        //if (@event is InputEventMouseButton eventMouseButton1)
        //{
        //    if (eventMouseButton1.ButtonIndex == MouseButton.WheelUp && eventMouseButton1.Pressed == true && size < 100)
        //    {
        //        //SetSize(size + 0.05F);
        //        //GD.Print(size);
        //        Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");
        //        camera.Zoom = new Vector2(camera.Zoom.X + 0.1F, camera.Zoom.Y + 0.1F);

        //    }
        //    else if (eventMouseButton1.ButtonIndex == MouseButton.WheelDown && eventMouseButton1.Pressed == true && size > 0.01)
        //    {
        //        //SetSize(size - 0.01F);
        //        //GD.Print(size);
        //        Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");
        //        camera.Zoom = new Vector2(camera.Zoom.X - 0.1F, camera.Zoom.Y - 0.1F);
        //    }
        //}

        //public void RegisterPlayerBodyMovement(ref Vector2 playerBodyVelocity)
        //{
        //    if (Input.IsActionPressed("move_right"))
        //    {
        //        playerBodyVelocity.X += 1;
        //    }
        //    if (Input.IsActionPressed("move_left"))
        //    {
        //        playerBodyVelocity.X -= 1;
        //    }
        //}

        //public void SetPlayerBodyPosition(Vector2 playerBodyVelocity, double delta)
        //{
        //    body.Position += playerBodyVelocity * (float)delta;
        //    body.Position = new Vector2(
        //        x: Mathf.Clamp(body.Position.X, 55, ScreenSize.X - 55),
        //        y: Mathf.Clamp(body.Position.Y, 65, ScreenSize.Y - 65)
        //    );
        //}


        //public void SetPlayerSoulPosition(double delta)
        //{
        //    Vector2 desiredPos = GetCursorPos();
        //    SetPlayerTether(desiredPos + body.Position);
        //    scout.Position = soul.Position;

        //    KinematicCollision2D collision = scout.MoveAndCollide((desiredPos + body.Position - soul.Position) * 1F);
        //    if (collision != null)
        //    {
        //        DoAdditionalSoulMovements(collision);
        //    }



        //    soul.Position = scout.Position;
        //    soul.MoveAndSlide();
        //}

        //public void MovePlayerBody(double delta)
        //{
        //    Vector2 velocity = body.Velocity;
        //    velocity = MovePlayerBodyVertically(velocity);
        //    velocity = MovePlayerBodyHorizontally(velocity);
        //    SetPlayerBodySprite(velocity);

        //    body.Velocity = velocity;
        //    body.MoveAndSlide();
        //}

        //public Vector2 MovePlayerBodyVertically(Vector2 velocity)
        //{
        //    float gravity = 25;
        //    float gravityMod;

        //    // Handle Jump.
        //    if (Input.IsActionJustPressed("jump") && body.IsOnFloor())
        //    {
        //        velocity.Y += -1500F;
        //    }

        //    //Handle gravity speed
        //    if (Input.IsActionPressed("jump"))
        //    {
        //        gravityMod = 1;
        //    }
        //    else
        //    {
        //        gravityMod = 7;
        //    }

        //    // Add the gravity.
        //    if (!body.IsOnFloor())
        //    {
        //        if (velocity.Y <= 0 && (velocity.Y += (gravity * gravityMod)) > 0)
        //        {
        //            velocity.Y = 0;
        //        }
        //        if (velocity.Y >= 0)
        //        {
        //            //if (prevGravityMod == 1 && gravityMod == 7)
        //            if (prevGravityMod == 1 && gravityMod == 7)
        //            {
        //                velocity.Y *= 7;
        //            }
        //            //else if (prevGravityMod == 7 && gravityMod == 1)
        //            else if (prevGravityMod == 7 && gravityMod == 1)
        //            {
        //                velocity.Y *= 0.14285F;
        //            }
        //        }
        //        velocity.Y += gravity * gravityMod;
        //        //velocity.Y += (gravity * (float)delta) * gravityMod;
        //        if (velocity.Y > (gravity * gravityMod) * 10)
        //        {
        //            velocity.Y = (gravity * gravityMod) * 10;
        //        }
        //        prevGravityMod = gravityMod;
        //    }
        //    return velocity;
        //}

        //public Vector2 MovePlayerBodyHorizontally(Vector2 velocity)
        //{
        //    float sneakMod = 1;

        //    if (Input.IsActionPressed("sneak"))
        //    {
        //        sneakMod = 0.5F;
        //    }

        //    Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        //    if (direction != Vector2.Zero)
        //    {
        //        velocity.X = (direction.X * 1) * (speed * sneakMod);
        //    }
        //    else
        //    {
        //        velocity.X = Mathf.MoveToward(body.Velocity.X, 0, (speed * sneakMod));
        //    }
        //    return velocity;
        //}

        //if (@event is InputEventMouseButton eventMouseButton2 && eventMouseButton2.Pressed == true && energy >= 1)
        //{
        //    body.Position = soul.Position;
        //    UpdateEnergyBar(-200);
        //    UpdateHealthAlpha();
        //}

        //if (@event is InputEventMouseMotion eventMouseMotion)
        //{
        //    AnimatedSprite2D playerSoulSprite = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulSprite");
        //}

        //public Vector2 GetCursorPos()
        //{
        //    Vector2 cursorPos = body.GetLocalMousePosition();

        //    //if distance between cursor and playerBody is greater than 150 (pixels?), then set cursorPos (Not the actual cursor) to be 150 away from playerBody, pointing towards cursor
        //    double cursorDistance = Math.Sqrt(Math.Pow(cursorPos.X - 0, 2) + Math.Pow(cursorPos.Y - 0, 2));
        //    if (cursorDistance > 350)
        //    {
        //        double multiplier = 350 / cursorDistance;
        //        cursorPos = new Vector2((float)(cursorPos.X * multiplier), (float)(cursorPos.Y * multiplier)); //HERE
        //    }
        //    return cursorPos;
        //}

        //public void DoSecondMovement(KinematicCollision2D collision, Vector2 desiredPos)
        //{
        //    //bool goodPos = false;

        //    //while (!goodPos)
        //    //{
        //    for (int i = 0; i < 20; i++)
        //    {
        //        double degrees = RadToDeg(collision.GetAngle());

        //        Vector2 secondMovement = new Vector2(0, 0);
        //        if ((degrees >= 90 && degrees <= 135) && soul.Position.Y > scout.Position.Y)
        //        {
        //            secondMovement = new Vector2(0, -5);
        //            //GD.Print("up");
        //        }
        //        else if ((degrees >= 45 && degrees <= 90) && soul.Position.Y < scout.Position.Y)
        //        {
        //            secondMovement = new Vector2(0, 5);
        //            //GD.Print("down");
        //        }
        //        else
        //        {
        //            if (degrees < 135)
        //            {
        //                degrees += 135;
        //            }
        //            if ((degrees >= 135 && degrees <= 180) && soul.Position.X > scout.Position.X)
        //            {
        //                secondMovement = new Vector2(-5, 0);
        //                //GD.Print("left");
        //            }
        //            else if ((degrees >= 135 && degrees <= 180) && soul.Position.X < scout.Position.X)
        //            {
        //                secondMovement = new Vector2(5, 0);
        //                //GD.Print("right");
        //            }
        //        }
        //        KinematicCollision2D secondCollision = scout.MoveAndCollide(secondMovement);

        //        if (secondCollision != null)
        //        {
        //            //GD.Print("collision");
        //            //goodPos = true;
        //        }
        //        else
        //        {
        //            //GD.Print("No collision");
        //            if (secondMovement == new Vector2(0, -5))
        //            {
        //                //GD.Print("up 2");
        //                if (scout.Position.Y <= desiredPos.Y)
        //                {
        //                    scout.Position = new Vector2(scout.Position.X, desiredPos.Y);
        //                    //goodPos = true;
        //                    //GD.Print("up 3");
        //                }
        //            }
        //            else if (secondMovement == new Vector2(0, 5))
        //            {
        //                //GD.Print("down 2");
        //                if (scout.Position.Y >= desiredPos.Y)
        //                {
        //                    scout.Position = new Vector2(scout.Position.X, desiredPos.Y);
        //                    //goodPos = true;
        //                    //GD.Print("down 3");
        //                }
        //            }
        //            else if (secondMovement == new Vector2(-5, 0))
        //            {
        //                //GD.Print("left 2");
        //                if (scout.Position.X <= desiredPos.X)
        //                {
        //                    scout.Position = new Vector2(desiredPos.X, scout.Position.Y);
        //                    //goodPos = true;
        //                    //GD.Print("left 3");
        //                }
        //            }
        //            else if (secondMovement == new Vector2(5, 0))
        //            {
        //                //GD.Print("right 2");
        //                if (scout.Position.X >= desiredPos.X)
        //                {
        //                    scout.Position = new Vector2(desiredPos.X, scout.Position.Y);
        //                    //goodPos = true;
        //                    //GD.Print("right 3");
        //                }
        //            }
        //            else
        //            {


        //                //GD.Print("no direction");
        //            }
        //        }
        //        //GD.Print("frame: ", frame++);
        //    }
        //    //}
        //}

        //public void DoSecondMovement2(KinematicCollision2D collision)
        //{
        //    Vector2 collisionPos = collision.GetPosition();
        //    Vector2 desiredPos = body.GetGlobalMousePosition();
        //    int verticalmovement;
        //    int horizontalmovement;

        //    if (collisionPos.X < desiredPos.X)
        //    {
        //        horizontalmovement = 10;
        //    }
        //    else if (collisionPos.X == desiredPos.X)
        //    {
        //        horizontalmovement = 0;
        //    }
        //    else
        //    {
        //        horizontalmovement = -10;
        //    }

        //    if (collisionPos.Y < desiredPos.Y)
        //    {
        //        verticalmovement = 10;
        //    }
        //    else if (collisionPos.Y == desiredPos.Y)
        //    {
        //        verticalmovement = 0;
        //    }
        //    else
        //    {
        //        verticalmovement = -10;
        //    }

        //    Vector2 secondMovement = new Vector2(horizontalmovement, verticalmovement);
        //    for (int i = 0; i < 20; i++)
        //    {
        //        KinematicCollision2D secondCollision = scout.MoveAndCollide(secondMovement);
        //    }

        //}

        //public void DoAdditionalSoulMovements(KinematicCollision2D collision)
        //{
        //    Vector2 desiredPos = body.GetGlobalMousePosition();
        //    int verticalmovement;
        //    int horizontalmovement;
        //    string verticalStatus = "active";
        //    string horizontalStatus = "active";





        //    if (scout.GlobalPosition.X < desiredPos.X)
        //    {
        //        horizontalmovement = 1;
        //    }
        //    else if (scout.GlobalPosition.X == desiredPos.X)
        //    {
        //        horizontalmovement = 0;
        //        horizontalStatus = "done";
        //    }
        //    else
        //    {
        //        horizontalmovement = -1;
        //    }

        //    if (scout.GlobalPosition.Y < desiredPos.Y)
        //    {
        //        verticalmovement = 1;
        //    }
        //    else if (scout.GlobalPosition.Y == desiredPos.Y)
        //    {
        //        verticalmovement = 0;
        //        verticalStatus = "done";
        //    }
        //    else
        //    {
        //        verticalmovement = -1;
        //    }

        //    while (horizontalStatus != "done" && verticalStatus != "done")
        //    {
        //        DoAdditionalSoulMovement(horizontalmovement, verticalmovement, ref horizontalStatus, ref verticalStatus, desiredPos);
        //    }
        //}

        //public void DoAdditionalSoulMovement2(int horizontalMovement, int verticalMovement, ref string horizontalStatus, ref string verticalStatus, Vector2 desiredPos)
        //{
        //    if (horizontalStatus != "done")
        //    {
        //        KinematicCollision2D horizontalCollision = scout.MoveAndCollide(new Vector2(horizontalMovement, 0));
        //        if (horizontalCollision != null)
        //        {
        //            horizontalStatus = "collision";
        //        }
        //        if (horizontalMovement < 0 && scout.Position.X + body.Position.X <= desiredPos.X)
        //        {
        //            scout.Position = new Vector2(desiredPos.X, scout.Position.Y);
        //            horizontalStatus = "done";
        //        }
        //        else if (horizontalMovement > 0 && scout.Position.X + body.Position.X >= desiredPos.X)
        //        {
        //            scout.Position = new Vector2(desiredPos.X, scout.Position.Y);
        //            horizontalStatus = "done";
        //        }
        //    }

        //    if (verticalStatus != "done")
        //    {
        //        KinematicCollision2D verticalCollision = scout.MoveAndCollide(new Vector2(0, verticalMovement));
        //        if (verticalCollision != null)
        //        {
        //            verticalStatus = "collision";
        //        }
        //        if (verticalMovement < 0 && scout.Position.Y + body.Position.Y <= desiredPos.Y)
        //        {
        //            scout.Position = new Vector2(scout.Position.X, desiredPos.Y);
        //            verticalStatus = "done";
        //        }
        //        else if (verticalMovement > 0 && scout.Position.Y + body.Position.Y >= desiredPos.Y)
        //        {
        //            scout.Position = new Vector2(scout.Position.X, desiredPos.Y);
        //            verticalStatus = "done";
        //        }
        //    }

        //    if ((horizontalStatus == "collision" || horizontalStatus == "done") && (verticalStatus == "collision" || verticalStatus == "done"))
        //    {
        //        horizontalStatus = "done";
        //        verticalStatus = "done";
        //    }
        //    else if (horizontalStatus == "collision" && verticalStatus == "active")
        //    {
        //        horizontalStatus = "active";
        //    }
        //    else if (horizontalStatus == "active" && verticalStatus == "collision")
        //    {
        //        verticalStatus = "active";
        //    }
        //}

        //public Vector2 CalculateDegreesBetweenPoints(Vector2 pointA, Vector2 pointB)
        //{
        //    float xDiff = pointB.X - pointA.X;
        //    float yDiff = pointB.Y - pointA.Y;
        //    double theta = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

        //    double r = 30;

        //    double x = pointA.X + r * Math.Cos(theta * Math.PI / 180);
        //    double y = pointA.Y + r * Math.Sin(theta * Math.PI / 180);

        //    return new Vector2((float)x, (float)y);
        //}






        //public void TryToUncrouch2()
        //{
        //    AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        //    CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        //    Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");





        //    Vector2 prevBodyPos = body.GlobalPosition;
        //    float uncrouchMod = 1;

        //    KinematicCollision2D uncrouchCollision = body.MoveAndCollide(new Vector2(0, (-32 * (1 - prevUncrouchMod))));
        //    if (uncrouchCollision is not null)
        //    {
        //        Vector2 nextBodyPos = body.GlobalPosition - prevBodyPos;
        //        uncrouchMod = (nextBodyPos.Y / 32) * -1;

        //    }


        //    //body.Position = new Vector2(body.Position.X, body.Position.Y + 30F * size);


        //    bodySprite.Position = new Vector2(bodySprite.Position.X, (-10F + (10F * uncrouchMod)) * size);
        //    bodyCollision.Position = new Vector2(bodySprite.Position.X, ((15F * uncrouchMod) * size));
        //    bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, ((1F * (0.68F + 0.32F * uncrouchMod)) * size));
        //    camera.Position = new Vector2(0, (-30 + (30 * uncrouchMod)));

        //    if (uncrouchMod != 1)
        //    {
        //        tryingToUncrouch = true;
        //    }
        //    else
        //    {
        //        isCrouched = false;
        //        tryingToUncrouch = false;
        //    }
        //    prevUncrouchMod = uncrouchMod;
        //    GD.Print(uncrouchMod);
        //}



        //public void TryToUncrouch()
        //{
        //    AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        //    CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        //    Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

        //    bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F * size);
        //    if (body.IsOnFloor() && body.IsOnCeiling())
        //    {
        //        bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 0.68F * size);
        //        tryingToUncrouch = true;
        //    }
        //    else
        //    {
        //        bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
        //        bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F * size);
        //        body.Position = new Vector2(body.Position.X, body.Position.Y - 30F * size);
        //        camera.Position = new Vector2(0, 0);
        //        isCrouched = false;
        //        tryingToUncrouch = false;
        //    }

        //    //Crouched
        //    //bodyCollision.Position = 0, 0
        //    //bodyCollision.Scale = 0, (0.68F * size)

        //    //Half uncrouched?
        //    //bodyCollision.Position = 0, (7.5F * size)
        //    //bodyCollision.Scale = 0, (0.84F * size)

        //    //Uncrouched
        //    //bodyCollision.Position = 0, (15F * size)
        //    //bodyCollision.Scale = 0, (1F * size)


        //    //bodyCollision.Position = 0, ((15F * unCrouchPercent) * size)
        //    //bodyCollision.Scale = 0, ((1F * (0.68 + 0.32 * unCrouchPercent )) * size)

        //    //0. (You are crouched)
        //    //1. MoveAndCollide bodycollision from (0, 0) to (0, -32)
        //    //2. unCrouchPercent = bodyCollision.Position.Y / 32
        //    //   (If it goes halfway, then it will be 16 / 32 = 0.5)
        //    //3. bodyCollision.Position = new Vector(0, ((15F * unCrouchPercent) * size)
        //    //   bodyCollision.Scale = 0, ((1F * (0.68 + 0.32 * unCrouchPercent)) * size)

        //}

        //public void ToggleCrouchMode()
        //{

        //    bool crouch = Input.IsActionPressed("sneak");

        //    AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        //    CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        //    Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

        //    if (!isCrouched && crouch && body.IsOnFloor())
        //    {
        //        bodySprite.Position = new Vector2(bodySprite.Position.X, -10F);
        //        bodyCollision.Position = new Vector2(bodySprite.Position.X, 0F);
        //        bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 0.68F);
        //        body.Position = new Vector2(body.Position.X, body.Position.Y + 30F);
        //        camera.Position = new Vector2(0, -30);
        //        isCrouched = true;
        //    }
        //    else if (isCrouched && !crouch && body.IsOnFloor())
        //    {
        //        bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
        //        bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F);
        //        bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F);
        //        body.Position = new Vector2(body.Position.X, body.Position.Y - 30F);
        //        camera.Position = new Vector2(0, 0);
        //        isCrouched = false;
        //    }
        //    else if (isCrouched && !body.IsOnFloor())
        //    {
        //        bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
        //        bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F);
        //        bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F);
        //        body.Position = new Vector2(body.Position.X, body.Position.Y - 30F);
        //        camera.Position = new Vector2(0, 0);
        //        isCrouched = false;
        //    }
        //    SetBodySpriteImage(body.Velocity, bodySprite);
        //}

        //public void ToggleCrouchMode2()
        //{
        //    bool crouch = Input.IsActionPressed("sneak");

        //    AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        //    CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        //    Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

        //    if (!isCrouched && crouch && body.IsOnFloor())
        //    {
        //        bodySprite.Position = new Vector2(bodySprite.Position.X, -10F * size);
        //        bodyCollision.Position = new Vector2(bodySprite.Position.X, 0F);
        //        bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 0.68F * size);
        //        body.Position = new Vector2(body.Position.X, body.Position.Y + 30F * size);
        //        camera.Position = new Vector2(0, -30);
        //        isCrouched = true;
        //    }
        //    else if (isCrouched && !crouch && body.IsOnFloor())
        //    {
        //        bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
        //        bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F * size);
        //        bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F * size);
        //        body.Position = new Vector2(body.Position.X, body.Position.Y - 30F * size);
        //        camera.Position = new Vector2(0, 0);
        //        isCrouched = false;
        //    }
        //    else if (isCrouched && !body.IsOnFloor())
        //    {
        //        bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
        //        bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F * size);
        //        bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F * size);
        //        body.Position = new Vector2(body.Position.X, body.Position.Y - 30F * size);
        //        camera.Position = new Vector2(0, 0);
        //        isCrouched = false;
        //    }
        //    SetBodySpriteImage(body.Velocity, bodySprite);
        //}

        //public void SetPlayerBodyStatus()
        //{
        //    if (body.IsOnFloor())
        //    {
        //        if (!isCrouched)
        //        {
        //            if (body.Velocity.X != 0)
        //            {
        //                bodyStatus = PlayerBodyStatus.Walking;
        //            }
        //            else
        //            {
        //                bodyStatus = PlayerBodyStatus.Idle;
        //            }
        //        }
        //        else
        //        {
        //            CharacterBody2D crouchScout = GetNode<CharacterBody2D>("PlayerBody/PlayerBodyCrouch");
        //            KinematicCollision2D collision = crouchScout.MoveAndCollide(new Vector2(0, 0));
        //            crouchScout.Position = new Vector2(0, -47);

        //            if (collision == null)
        //            {
        //                bodyStatus = PlayerBodyStatus.Crouching;
        //            }
        //            else
        //            {
        //                bodyStatus = PlayerBodyStatus.CrouchingCramped;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (body.Velocity.Y < 0)
        //        {
        //            if (body.Velocity.Y < -1500)
        //            {
        //                bodyStatus = PlayerBodyStatus.HighJumping;
        //            }
        //            else if (body.Velocity.Y < 0)
        //            {
        //                bodyStatus = PlayerBodyStatus.Jumping;
        //            }

        //        }
        //        else
        //        {
        //            if (prevGravityMod == 1)
        //            {
        //                bodyStatus = PlayerBodyStatus.Gliding;
        //            }
        //            else if (prevGravityMod > 7)
        //            {
        //                bodyStatus = PlayerBodyStatus.FastFalling;
        //            }
        //            else if (prevGravityMod > 1)
        //            {
        //                bodyStatus = PlayerBodyStatus.Falling;
        //            }
        //        }
        //    }
        //}

        //public void SetBodySpriteImage(Vector2 velocity, AnimatedSprite2D bodySprite)
        //{
        //    //if (body.IsOnFloor() && !isCrouched)
        //    //{
        //    //    bodySprite.Animation = "Walk";
        //    //}
        //    //else if ((body.IsOnFloor() && isCrouched) || (crouchCollisionSinceLastCheck && beganJumpThisFrame))
        //    //{
        //    //    bodySprite.Animation = "Crouch";
        //    //}
        //    //else if (body.GlobalPosition.Y > velocity.Y)
        //    //{
        //    //    bodySprite.Animation = "Jump";
        //    //}
        //    //else if (body.GlobalPosition.Y < velocity.Y && prevGravityMod == 1)
        //    //{
        //    //    bodySprite.Animation = "Glide";
        //    //}
        //    //else if (body.GlobalPosition.Y < velocity.Y && (prevGravityMod == 7 || prevGravityMod == 6))
        //    //{
        //    //    bodySprite.Animation = "Fall";
        //    //}
        //}






        //public void SetSize(float size = 1F)
        //{
        //    AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        //    AnimatedSprite2D bodyVisual = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodyVisual");
        //    CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        //    AnimatedSprite2D soulSprite = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulSprite");
        //    AnimatedSprite2D soulVisual = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulVisual");
        //    CollisionShape2D soulCollision = GetNode<CollisionShape2D>("PlayerSoul/PlayerSoulCollision");
        //    AnimatedSprite2D scoutVisual = GetNode<AnimatedSprite2D>("PlayerScout/PlayerScoutVisual");
        //    CollisionShape2D scoutCollision = GetNode<CollisionShape2D>("PlayerScout/PlayerScoutCollision");
        //    Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

        //    bodySprite.Scale = new Vector2(4F * size, 4F * size);
        //    bodyVisual.Scale = new Vector2(0.16F * size, 0.16F * size);
        //    bodyCollision.Position = new Vector2(0F, 15F * size);
        //    bodyCollision.Scale = new Vector2(1F * size, 1F * size);
        //    soulSprite.Scale = new Vector2(3F * size, 3F * size);
        //    soulVisual.Scale = new Vector2(0.16F * size, 0.16F * size);
        //    soulCollision.Scale = new Vector2(1F * size, 1F * size);
        //    scoutVisual.Scale = new Vector2(0.16F * size, 0.16F * size);
        //    scoutCollision.Scale = new Vector2(1F * size, 1F * size);
        //    camera.Zoom = new Vector2(0.75F / size, 0.75F / size);

        //    this.size = size;
        //    UpdateHealthOrb(0);
        //    UpdateEnergyOrb(0);
        //}


        //public void SetHealthOrbSize()
        //{
        //    AnimatedSprite2D bodyHealth = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodyHealth");
        //    if (health > 0)
        //    {
        //        double newSize = 0.16F * (health / maxHealth) * 1F; //1F is size
        //        bodyHealth.Scale = new Vector2((float)newSize, (float)newSize);
        //    }
        //    else
        //    {
        //        bodyHealth.Scale = new Vector2(0, 0);
        //    }
        //}


        //public void UpdateHealthOrb(double addedHealth)
        //{
        //    AddHealth(addedHealth);
        //    SetHealthOrbSize();
        //}

        //public void SetEnergyOrbSize()
        //{
        //    AnimatedSprite2D soulEnergy = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulEnergy");
        //    if (energy > 0)
        //    {
        //        double newSize = 0.16F * (energy / maxEnergy) * 1F; //1F is size
        //        soulEnergy.Scale = new Vector2((float)newSize, (float)newSize);
        //    }
        //    else
        //    {
        //        soulEnergy.Scale = new Vector2(0, 0);
        //    }
        //}

        //public void UpdateEnergyOrb(double addedEnergy)
        //{
        //    AddEnergy(addedEnergy);
        //    SetEnergyOrbSize();
        //}

        //public void CheckAttack()
        //{
        //    if (Input.IsActionJustPressed("mouse1"))
        //    {
        //        if (playerMode == PlayerMode.Body)
        //        {
        //            DoMouse1Body();
        //        }
        //        else // (playerMode == PlayerMode.Soul)
        //        {
        //            DoMouse1Soul();
        //        }
        //    }

        //    if (Input.IsActionJustPressed("mouse2"))
        //    {
        //        if (playerMode == PlayerMode.Body)
        //        {
        //            DoMouse2Body();
        //        }
        //        else // (playerMode == PlayerMode.Soul)
        //        {
        //            DoMouse2Soul();
        //        }
        //    }

        //    DoAttack();
        //}


        //public void DoAttack()
        //{
        //    if (postAttackTimer > 0)
        //    {
        //        postAttackTimer--;
        //    }
        //    if (postAttackTimer == 0 && preAttackTimer > 0)
        //    {
        //        preAttackTimer--;
        //    }
        //    if (preAttackTimer == 0 && nextAttack != Attack.None)
        //    {
        //        nextAttack = Attack.None;
        //    }
        //}

        //public void DecreaseAttackRecovery()
        //{

        //}

        //public void DoMouse1Body()
        //{
        //    if (postAttackTimer == 0)
        //    {
        //        //var scene = GD.Load<PackedScene>("res://Projectile.tscn");
        //        //Projectile instance = (Projectile)scene.Instantiate();
        //        //instance.Boo();
        //        //var scene2 = ResourceLoader.Load<PackedScene>("res://Projectile.cs").Instantiate();

        //        PackedScene Projectiles = GD.Load<PackedScene>("res://projectile.tscn");
        //        Projectile projectile = (Projectile)Projectiles.Instantiate();
        //        projectile.GlobalPosition = body.GlobalPosition;
        //        Node node = GetParent(); //Start
        //                                 //node.AddChild(projectile);
        //        body.AddChild(projectile);
        //        projectile.GlobalPosition = body.GlobalPosition;
        //        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");

        //        projectile.SetData(this, 0, new List<int>() { 0, 1 }, 100, 0, 20, false, 5);
        //        postAttackTimer = 15;

        //        if (Input.IsActionPressed("up"))
        //        {
        //            projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X, projectile.GlobalPosition.Y - 100);
        //        }
        //        else if (bodySprite.FlipH)
        //        {
        //            projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X - 100, projectile.GlobalPosition.Y);
        //        }
        //        else
        //        {
        //            projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X + 100, projectile.GlobalPosition.Y);
        //        }
        //    }
        //}

        //public void DoMouse1Soul()
        //{

        //}

        //public void DoMouse2Body()
        //{
        //    if (energy > 0 && postAttackTimer == 0)
        //    {
        //        //var scene = GD.Load<PackedScene>("res://Projectile.tscn");
        //        //Projectile instance = (Projectile)scene.Instantiate();
        //        //instance.Boo();
        //        //var scene2 = ResourceLoader.Load<PackedScene>("res://Projectile.cs").Instantiate();

        //        PackedScene Projectiles = GD.Load<PackedScene>("res://projectile.tscn");
        //        Projectile projectile = (Projectile)Projectiles.Instantiate();
        //        projectile.GlobalPosition = body.GlobalPosition;
        //        Node node = GetParent(); //Start
        //                                 //node.AddChild(projectile);
        //        body.AddChild(projectile);
        //        projectile.GlobalPosition = body.GlobalPosition;
        //        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");

        //        projectile.SetData(this, 0, new List<int>() { 0, 1 }, 100, 0, 0, true, 5);
        //        UpdateEnergyOrb(-100);
        //        postAttackTimer = 35;

        //        if (Input.IsActionPressed("up"))
        //        {
        //            projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X, projectile.GlobalPosition.Y - 100);
        //        }
        //        else if (bodySprite.FlipH)
        //        {
        //            projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X - 100, projectile.GlobalPosition.Y);
        //        }
        //        else
        //        {
        //            projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X + 100, projectile.GlobalPosition.Y);
        //        }
        //    }
        //}

        //public void DoMouse2Soul()
        //{

        //}


    //        public override void _Process(double delta)
    //{

    //}





































































}
}

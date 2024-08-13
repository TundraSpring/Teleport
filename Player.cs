using Godot;

using static Godot.Mathf;

using System;
using System.ComponentModel.Design;
using System.Reflection;
using static Godot.TextServer;

public partial class Player : Node2D
{
    public float speed = 800; // How fast the player will move (pixels/sec).
    public float JumpVelocity = -1500F;
    public float size;
    public Vector2 prevEventMousePos = new Vector2(0, 0);
    public float gravity = 25F;
    public float prevGravityMod = 7;
    public bool hasMoved = false;
    public bool isCrouched = false;
    public bool tryingToUncrouch = false;

    public double energy = 400;
    public double maxEnergy = 400;
    public double health = 100;
    public double maxHealth = 100;
    public PlayerMode playerMode;
    public bool canGlide = false;

    public CharacterBody2D body;
    public CharacterBody2D soul;
    public CharacterBody2D tether;
    public CharacterBody2D scout;

    [Signal]
    public delegate void InteractEventHandler();

    public override void _Ready()
	{
        SetPlayerMode(PlayerMode.Body);
        SetSize(1F);
        body = GetNode<CharacterBody2D>("PlayerBody");
        soul = GetNode<CharacterBody2D>("PlayerSoul");
        tether = GetNode<CharacterBody2D>("PlayerTether");
        scout = GetNode<CharacterBody2D>("PlayerScout");
    }

    public override void _PhysicsProcess(double delta)
    {
        RestoreEnergy();
        MoveBody(delta);
        SetSoulPosition(delta);
    }

    public override void _Process(double delta)
	{

    }

    public void SetData(float size, double energy, double health, PlayerMode playerMode, bool canGlide)
    {

    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("teleport") && energy >= 1)
        {
            Teleport(soul.Position);
            UpdateEnergyOrb(-200);
            UpdateHealthOrb(-5);
        }
        else if (Input.IsActionJustPressed("switch_player_mode"))
        {
            if (playerMode == PlayerMode.Body)
            {
                SetPlayerMode(PlayerMode.Soul);
            }
            else
            {
                SetPlayerMode(PlayerMode.Body);
            }
        }
        else if (Input.IsActionJustPressed("interact"))
        {
            EmitSignal(SignalName.Interact);
        }
        else if (Input.IsActionJustPressed("sneak"))
        {
            hasMoved = false;
            ToggleCrouchMode3();
        }
        else if (Input.IsActionJustReleased("sneak"))
        {
            ToggleCrouchMode3();
            if (hasMoved == false)
            {
                body.Position = new Vector2(body.Position.X, body.Position.Y + 1);
                body.MoveAndSlide();
                hasMoved = true;
            }
        }
    }

    public void Teleport(Vector2 destination)
    {
        body.Position = destination;
    }

    public void SetCameraLimits(int left, int top, int right, int bottom)
    {
        Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");
        camera.LimitLeft = left;
        camera.LimitTop = top;
        camera.LimitRight = right;
        camera.LimitBottom = bottom; 
    }

    public void SetPlayerMode(PlayerMode playerMode)
    {
        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        AnimatedSprite2D soulSprite = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulSprite");
        AnimatedSprite2D bodyHealth = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodyHealth");
        AnimatedSprite2D soulEnergy = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulEnergy");

        if (playerMode == PlayerMode.Body)
        {
            bodySprite.Modulate = new Godot.Color(1, 1, 1, 1F);
            soulSprite.Modulate = new Godot.Color(1, 1, 1, 0.5F);
            bodyHealth.Modulate = new Godot.Color(3204513727);
            soulEnergy.Modulate = new Godot.Color(2952784831);
        }
        else
        {
            bodySprite.Modulate = new Godot.Color(1, 1, 1, 0.333F);
            soulSprite.Modulate = new Godot.Color(1, 1, 1, 1F);
            bodyHealth.Modulate = new Godot.Color(3204513727);
            soulEnergy.Modulate = new Godot.Color(2952784831);
        }

        this.playerMode = playerMode;
    }

    public void SetSize(float size = 1F)
    {
        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        AnimatedSprite2D bodyVisual = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodyVisual");
        CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        AnimatedSprite2D soulSprite = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulSprite");
        AnimatedSprite2D soulVisual = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulVisual");
        CollisionShape2D soulCollision = GetNode<CollisionShape2D>("PlayerSoul/PlayerSoulCollision");
        AnimatedSprite2D scoutVisual = GetNode<AnimatedSprite2D>("PlayerScout/PlayerScoutVisual");
        CollisionShape2D scoutCollision = GetNode<CollisionShape2D>("PlayerScout/PlayerScoutCollision");
        Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

        bodySprite.Scale = new Vector2(4F * size, 4F * size);
        bodyVisual.Scale = new Vector2(0.16F * size, 0.16F * size);
        bodyCollision.Position = new Vector2(0F, 15F * size);
        bodyCollision.Scale = new Vector2(1F * size, 1F * size);
        soulSprite.Scale = new Vector2(3F * size, 3F * size);
        soulVisual.Scale = new Vector2(0.16F * size, 0.16F * size);
        soulCollision.Scale = new Vector2(1F * size, 1F * size);
        scoutVisual.Scale = new Vector2(0.16F * size, 0.16F * size);
        scoutCollision.Scale = new Vector2(1F * size, 1F * size);
        camera.Zoom = new Vector2(0.75F / size, 0.75F / size);

        this.size = size;
        UpdateHealthOrb(0);
        UpdateEnergyOrb(0);
    }

    public void RestoreEnergy()
    {
        if (body.IsOnFloor())
        {
            UpdateEnergyOrb(2);
        }
    }

    public void UpdateHealthOrb(double addedHealth)
    {
        AddHealth(addedHealth);
        SetHealthOrbSize();
    }

    public void AddHealth(double addedHealth)
    {
        health += addedHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void SetHealthOrbSize()
    {
        AnimatedSprite2D bodyHealth = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodyHealth");
        if (health > 0)
        {
            double newSize = 0.16F * (health / maxHealth) * size;
            bodyHealth.Scale = new Vector2((float)newSize, (float)newSize);
        }
        else
        {
            bodyHealth.Scale = new Vector2(0, 0);
        }
    }

    public void UpdateEnergyOrb(double addedEnergy)
    {
        AddEnergy(addedEnergy);
        SetEnergyOrbSize();
    }

    public void AddEnergy(double addedEnergy)
    {
        energy += addedEnergy;
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
    }

    public void SetEnergyOrbSize()
    {
        AnimatedSprite2D soulEnergy = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulEnergy");
        if (energy > 0)
        {
            double newSize = 0.16F * (energy / maxEnergy) * size;
            soulEnergy.Scale = new Vector2((float)newSize, (float)newSize);

            
            //double energyPercent = energy / maxEnergy;
            //GD.Print("energyPercent ", energyPercent);
            //double radiusSquared = energyPercent / Math.PI;
            //GD.Print("radiusSquared ", radiusSquared);
            //double radius = Math.Sqrt(radiusSquared);
            //GD.Print("radius ", radius);
            //double newSize = radius * 0.28F; //* 0.02835926161448825643677067973346F;
            //GD.Print("newSize ", newSize);
            //soulEnergy.Scale = new Vector2((float)newSize, (float)newSize);




        }
        else
        {
            soulEnergy.Scale = new Vector2(0, 0);
        }
    }

    public void MoveBody(double delta)
    {
        bool onGroundBefore = body.IsOnFloor();
        Vector2 velocity = body.Velocity;
        MoveBodyVertically(ref velocity);
        MoveBodyHorizontally(ref velocity);
        SetBodySprite(velocity);

        body.Velocity = velocity;
        body.MoveAndSlide();

        if (velocity != Vector2.Zero)
        {
            hasMoved = true;
        }
        bool onGroundAfter = body.IsOnFloor();
        if (onGroundBefore != onGroundAfter)
        {
            ToggleCrouchMode3();
        }
    }

    public void MoveBodyVertically(ref Vector2 velocity)
    {
        GetJumpVelocity(ref velocity);

        if (!body.IsOnFloor())
        {
            float gravity = this.gravity * size;
            float gravityMod = GetGravityMod(velocity);

            //Sets velocity to 0 when velocity goes in the positives to make sure gravityMod works correctly
            if (velocity.Y <= 0 && (velocity.Y += (gravity * gravityMod)) > 0)
            {
                velocity.Y = 0;
            }

            velocity = SetFallingVelocityToLightOrHeavy(velocity, gravityMod);
            velocity.Y += gravity * gravityMod;
            
            //The following is true if you are gliding downwards
            if (gravityMod == 1 && velocity.Y > 0)
            {
                UpdateEnergyOrb(-2);
            }

            //Makes sure velocity doesn't go beyond the maximum allowed
            if (velocity.Y > (gravity * gravityMod) * 10)
            {
                velocity.Y = (gravity * gravityMod) * 10;
            }

            prevGravityMod = gravityMod;
        }
    }

    public Vector2 SetFallingVelocityToLightOrHeavy(Vector2 velocity, float gravityMod)
    {
        if (velocity.Y >= 0)
        {
            //if (prevGravityMod == 1 && gravityMod == 7)
            if (prevGravityMod == 1 && gravityMod == 7)
            {
                velocity *= 7;
            }
            if (prevGravityMod == 6 && gravityMod == 7)
            {
                velocity *= 1.16666F;
            }

            //else if (prevGravityMod == 7 && gravityMod == 1)
            else if (prevGravityMod == 7 && gravityMod == 1)
            {
                velocity *= 0.14285F;
            }
            else if (prevGravityMod == 7 && gravityMod == 6)
            {
                velocity *= 0.85714F;
            }
        }
        return velocity;
    }

    public void GetJumpVelocity(ref Vector2 velocity)
    {
        if (Input.IsActionJustPressed("jump") && body.IsOnFloor())
        {
            velocity.Y += -1500F * size;
        }
    }

    public float GetGravityMod(Vector2 velocity)
    {
        //This code must be ran after jump velocity has been added
        if (Input.IsActionPressed("jump") && (velocity.Y <= 0))
        {
            return 1F;
        }
        else if (Input.IsActionPressed("jump") && (velocity.Y >= 0))
        {
            if (canGlide && energy >= 2)
            {
                return 1F;
            }
            else
            {
                return 6F;
            }
        }
        else
        {
            return 7F;
        }
    }

    public void MoveBodyHorizontally(ref Vector2 velocity)
    {
        if (tryingToUncrouch)
        {
            GD.Print("C");
            TryToUncrouch();
        }


        float sneakMod = 1F;
        if (Input.IsActionPressed("sneak"))
        {
            sneakMod = 0.5F;
        }

        //move_up and move_down do not add velocity, they are there to make sure the code functions
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * ((speed * size) * sneakMod);
        }
        else
        {
            velocity.X = Mathf.MoveToward(body.Velocity.X, 0, ((speed * size) * sneakMod));
        }
    }

    public void ToggleCrouchMode()
    {
        
        bool crouch = Input.IsActionPressed("sneak");

        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

        if (!isCrouched && crouch && body.IsOnFloor())
        {
            bodySprite.Position = new Vector2(bodySprite.Position.X, -10F);
            bodyCollision.Position = new Vector2(bodySprite.Position.X, 0F);
            bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 0.68F);
            body.Position = new Vector2(body.Position.X, body.Position.Y + 30F);
            camera.Position = new Vector2(0, -30);
            isCrouched = true;
        }
        else if (isCrouched && !crouch && body.IsOnFloor())
        {
            bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
            bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F);
            bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F);
            body.Position = new Vector2(body.Position.X, body.Position.Y - 30F);
            camera.Position = new Vector2(0, 0);
            isCrouched = false;
        }
        else if (isCrouched && !body.IsOnFloor())
        {
            bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
            bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F);
            bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F);
            body.Position = new Vector2(body.Position.X, body.Position.Y - 30F);
            camera.Position = new Vector2(0, 0);
            isCrouched = false;
        }
        SetBodySpriteImage(body.Velocity, bodySprite);
    }

    public void ToggleCrouchMode2()
    {
        bool crouch = Input.IsActionPressed("sneak");

        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

        if (!isCrouched && crouch && body.IsOnFloor())
        {
            bodySprite.Position = new Vector2(bodySprite.Position.X, -10F * size);
            bodyCollision.Position = new Vector2(bodySprite.Position.X, 0F);
            bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 0.68F * size);
            body.Position = new Vector2(body.Position.X, body.Position.Y + 30F * size);
            camera.Position = new Vector2(0, -30);
            isCrouched = true;
        }
        else if (isCrouched && !crouch && body.IsOnFloor())
        {
            bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
            bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F * size);
            bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F * size);
            body.Position = new Vector2(body.Position.X, body.Position.Y - 30F * size);
            camera.Position = new Vector2(0, 0);
            isCrouched = false;
        }
        else if (isCrouched && !body.IsOnFloor())
        {
            bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
            bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F * size);
            bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F * size);
            body.Position = new Vector2(body.Position.X, body.Position.Y - 30F * size);
            camera.Position = new Vector2(0, 0);
            isCrouched = false;
        }
        SetBodySpriteImage(body.Velocity, bodySprite);
    }


    public void ToggleCrouchMode3()
    {
        bool crouch = Input.IsActionPressed("sneak");

        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

        if (!isCrouched && crouch && body.IsOnFloor())
        {
            bodySprite.Position = new Vector2(bodySprite.Position.X, -10F * size);
            bodyCollision.Position = new Vector2(bodySprite.Position.X, 0F);
            bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 0.68F * size);
            body.Position = new Vector2(body.Position.X, body.Position.Y + 30F * size);
            camera.Position = new Vector2(0, -30);
            isCrouched = true;
        }
        else if (isCrouched && !crouch && body.IsOnFloor() || isCrouched && !body.IsOnFloor())
        {
            TryToUncrouch();
        }
        SetBodySpriteImage(body.Velocity, bodySprite);
    }

    public void TryToUncrouch()
    {
        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

        bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F * size);
        if (body.IsOnFloor() && body.IsOnCeiling())
        {
            bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 0.68F * size);
            tryingToUncrouch = true;
            GD.Print("B");
        }
        else
        {
            bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
            bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F * size);
            body.Position = new Vector2(body.Position.X, body.Position.Y - 30F * size);
            camera.Position = new Vector2(0, 0);
            isCrouched = false;
            tryingToUncrouch = false;
            GD.Print("A");
        }

            //Crouched
            //bodyCollision.Position = 0, 0
            //bodyCollision.Scale = 0, (0.68F * size)

            //Half uncrouched?
            //bodyCollision.Position = 0, (7.5F * size)
            //bodyCollision.Scale = 0, (0.84F * size)

            //Uncrouched
            //bodyCollision.Position = 0, (15F * size)
            //bodyCollision.Scale = 0, (1F * size)


            //bodyCollision.Position = 0, ((15F * unCrouchPercent) * size)
            //bodyCollision.Scale = 0, ((1F * (0.68 + 0.32 * unCrouchPercent )) * size)

            //0. (You are crouched)
            //1. MoveAndCollide bodycollision from (0, 0) to (0, -32)
            //2. unCrouchPercent = bodyCollision.Position.Y / 32
            //   (If it goes halfway, then it will be 16 / 32 = 0.5)
            //3. bodyCollision.Position = new Vector(0, ((15F * unCrouchPercent) * size)
            //   bodyCollision.Scale = 0, ((1F * (0.68 + 0.32 * unCrouchPercent)) * size)

    }


    public void SetBodySprite(Vector2 velocity)
    {
        velocity += body.GlobalPosition;
        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        SetBodySpriteDirection(velocity, bodySprite);
        SetBodySpriteImage(velocity, bodySprite);
    }

    public void SetBodySpriteDirection(Vector2 velocity, AnimatedSprite2D bodySprite)
    {
        if (velocity.X < body.GlobalPosition.X)
        {
            bodySprite.FlipH = true;
        }
        else if (velocity.X > body.GlobalPosition.X)
        {
            bodySprite.FlipH = false;
        }
    }

    public void SetBodySpriteImage(Vector2 velocity, AnimatedSprite2D bodySprite)
    {
        if (body.IsOnFloor() && !isCrouched)
        {
            bodySprite.Animation = "Walk";
        }
        else if (body.IsOnFloor() && isCrouched)
        {
            bodySprite.Animation = "Crouch";
        }
        else if (body.GlobalPosition.Y > velocity.Y)
        {
            bodySprite.Animation = "Jump";
        }
        else if (body.GlobalPosition.Y < velocity.Y && prevGravityMod == 1)
        {
            bodySprite.Animation = "Glide";
        }
        else if (body.GlobalPosition.Y < velocity.Y && (prevGravityMod == 7 || prevGravityMod == 6))
        {
            bodySprite.Animation = "Fall";
        }
    }

    public void SetSoulPosition(double delta)
    {
        PreventIllegalSoulDistance();
        Vector2 desiredPos = GetCursorPos() + body.Position;
        SetTether(desiredPos);

        scout.Position = soul.Position;
        KinematicCollision2D collision = scout.MoveAndCollide((desiredPos - soul.Position) * 1F);
        if (collision != null)
        {
            DoAdditionalSoulMovements(collision);
        }

        soul.Position = scout.Position;
        //soul.MoveAndSlide();

        SetSoulSpriteDirection();
    }

    public void PreventIllegalSoulDistance()
    {
        //if distance between cursor and playerBody is greater than 150 (pixels?), then set cursorPos (Not the actual cursor) to be 150 away from playerBody, pointing towards cursor
        double cursorDistance = Math.Sqrt(Math.Pow(soul.Position.X - body.Position.X, 2) + Math.Pow(soul.Position.Y - body.Position.Y, 2));
        if (cursorDistance > (375 * size))
        {
            soul.Position = body.Position;
        }
    }

    public Vector2 GetCursorPos()
    {
        Vector2 cursorPos = body.GetLocalMousePosition();

        //if distance between cursor and playerBody is greater than 150 (pixels?), then set cursorPos (Not the actual cursor) to be 150 away from playerBody, pointing towards cursor
        double cursorDistance = Math.Sqrt(Math.Pow(cursorPos.X - 0, 2) + Math.Pow(cursorPos.Y - 0, 2));
        if (cursorDistance > (350 * size))
        {
            double multiplier = (350 * size) / cursorDistance;
            cursorPos = new Vector2((float)(cursorPos.X * multiplier), (float)(cursorPos.Y * multiplier)); //HERE
        }
        return cursorPos;
    }

    public void SetTether(Vector2 desiredPos)
    {
        double distanceToSoul = Math.Sqrt(Math.Pow(desiredPos.X - soul.Position.X, 2) + Math.Pow(desiredPos.Y - soul.Position.Y, 2));
        double degreesToSoul = GetDegreesBetweenPoints(desiredPos, soul.Position) - 90;
        tether.RotationDegrees = (float)degreesToSoul;
        tether.Scale = new Vector2(tether.Scale.X, (float)(distanceToSoul * 0.0322));
        tether.Position = soul.Position;
    }

    public void DoAdditionalSoulMovements(KinematicCollision2D collision)
    {
        Vector2 desiredPos = GetCursorPos();
        desiredPos = body.ToGlobal(desiredPos);
        int verticalMovement = 0;
        int horizontalMovement = 0;
        string verticalStatus = "active";
        string horizontalStatus = "active";

        horizontalMovement = SetAdditionalHorizontalSoulMovement(desiredPos.X, horizontalMovement);
        verticalMovement = SetAdditionalVerticalSoulMovement(desiredPos.Y, verticalMovement);
        CheckIfAdditionalSoulMovementDirectionsAreDone(horizontalMovement, verticalMovement, ref horizontalStatus, ref verticalStatus);

        while (horizontalStatus != "done" && verticalStatus != "done")
        {
            DoAdditionalSoulMovement(horizontalMovement, verticalMovement, ref horizontalStatus, ref verticalStatus, desiredPos);
        }
    }

    public int SetAdditionalHorizontalSoulMovement(float x, int horizontalMovement)
    {
        if (scout.GlobalPosition.X < x)
        {
            horizontalMovement = 1;
        }
        else if (scout.GlobalPosition.X == x)
        {
            horizontalMovement = 0;
        }
        else
        {
            horizontalMovement = -1;
        }
        return horizontalMovement;
    }

    public int SetAdditionalVerticalSoulMovement(float y, int verticalMovement)
    {
        if (scout.GlobalPosition.Y < y)
        {
            verticalMovement = 1;
        }
        else if (scout.GlobalPosition.Y == y)
        {
            verticalMovement = 0;
        }
        else
        {
            verticalMovement = -1;
        }
        return verticalMovement;
    }

    public void CheckIfAdditionalSoulMovementDirectionsAreDone(int horizontalMovement, int verticalMovement, ref string horizontalStatus, ref string verticalStatus)
    {
        if (horizontalMovement == 0)
        {
            horizontalStatus = "done";
        }
        if (verticalMovement == 0)
        {
            verticalStatus = "done";
        }
    }

    public void DoAdditionalSoulMovement(int horizontalMovement, int verticalMovement, ref string horizontalStatus, ref string verticalStatus, Vector2 desiredPos)
    {
        if (horizontalStatus != "done")
        {
            KinematicCollision2D horizontalCollision = scout.MoveAndCollide(new Vector2(horizontalMovement, 0));
            if (horizontalCollision != null)
            {
                horizontalStatus = "collision";
            }
            if (horizontalMovement < 0 && scout.Position.X <= desiredPos.X)
            {
                scout.Position = new Vector2(desiredPos.X, scout.Position.Y);
                horizontalStatus = "done";
            }
            else if (horizontalMovement > 0 && scout.Position.X >= desiredPos.X)
            {
                scout.Position = new Vector2(desiredPos.X, scout.Position.Y);
                horizontalStatus = "done";
            }
        }

        if (verticalStatus != "done")
        {
            KinematicCollision2D verticalCollision = scout.MoveAndCollide(new Vector2(0, verticalMovement));
            if (verticalCollision != null)
            {
                verticalStatus = "collision";
            }
            if (verticalMovement < 0 && scout.Position.Y <= desiredPos.Y)
            {
                scout.Position = new Vector2(scout.Position.X, desiredPos.Y);
                verticalStatus = "done";
            }
            else if (verticalMovement > 0 && scout.Position.Y >= desiredPos.Y)
            {
                scout.Position = new Vector2(scout.Position.X, desiredPos.Y);
                verticalStatus = "done";
            }
        }

        if ((horizontalStatus == "collision" || horizontalStatus == "done") && (verticalStatus == "collision" || verticalStatus == "done"))
        {
            horizontalStatus = "done";
            verticalStatus = "done";
        }
        else if (horizontalStatus == "collision" && verticalStatus == "active")
        {
            horizontalStatus = "active";
        }
        else if (horizontalStatus == "active" && verticalStatus == "collision")
        {
            verticalStatus= "active";
        }
    }

    public void SetSoulSpriteDirection()
    {
        AnimatedSprite2D soulSprite = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulSprite");
        if (soul.Position < body.Position)
        {
            soulSprite.FlipH = false;
        }
        else if (soul.Position > body.Position)
        {
            soulSprite.FlipH = true;
        }
    }

    public double GetDegreesBetweenPoints(Vector2 pointA, Vector2 pointB)
    {
        float xDiff = pointB.X - pointA.X;
        float yDiff = pointB.Y - pointA.Y;
        return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
    }
}

using Godot;

using static Godot.Mathf;

using System;
using System.ComponentModel.Design;
using System.Reflection;
using static Godot.TextServer;
using System.IO;
using static Godot.OpenXRHand;
using GodotPlugins.Game;
using System.Collections.Generic;

public partial class Player : Node2D, IGameObject
{
    public float speed = 800; // How fast the player will move (pixels/sec).
    public float size;
    public Vector2 prevEventMousePos = new Vector2(0, 0);
    public float prevGravityMod = 7;
    public bool hasMoved = false;
    public bool beganJumpThisFrame = false;
    public int fastFallInputTimer = -1;
    public float? longJumpDir;

    public int preAttackTimer = 0;
    public int postAttackTimer = 0;
    public Attack previousAttack;
    public Attack nextAttack;

    public double energy = 400;
    public double maxEnergy = 400;
    public double health = 100;
    public double maxHealth = 100;
    public PlayerMode playerMode;
    public PlayerBodyStatus bodyStatus;
    public PlayerSoulStatus soulStatus = PlayerSoulStatus.Default;
    public int teleportInputTimer = 0;
    public bool canGlide = true;

    public CharacterBody2D body;
    public CharacterBody2D soul;
    public CharacterBody2D tether;
    public CharacterBody2D scout;

    [Signal]
    public delegate void InteractEventHandler();

    public override void _Ready()
	{
        body = GetNode<CharacterBody2D>("PlayerBody");
        soul = GetNode<CharacterBody2D>("PlayerSoul");
        tether = GetNode<CharacterBody2D>("PlayerTether");
        scout = GetNode<CharacterBody2D>("PlayerScout");

        SetPlayerMode(PlayerMode.Body);
        SetSoulStatus(PlayerSoulStatus.Default);
    }

    public void SetSoulStatus(PlayerSoulStatus newSoulStatus)
    {
        if (newSoulStatus == PlayerSoulStatus.Box)
        {
            soul.CollisionLayer = 290;
            soul.CollisionMask = 290;

            AnimatedSprite2D soulSprite = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulSprite");
            AnimatedSprite2D soulVisual = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulVisual");
            CollisionShape2D soulCollision = GetNode<CollisionShape2D>("PlayerSoul/PlayerSoulCollision");
            AnimatedSprite2D soulEnergy = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulEnergy");
            soulSprite.Animation = "Square";
            soulVisual.Animation = "Square";
            soulCollision.Shape = new RectangleShape2D();
            soulCollision.Scale = new Vector2(3.2F, 3.2F);
            soulEnergy.Animation = "Square";

            AnimatedSprite2D scoutVisual = GetNode<AnimatedSprite2D>("PlayerScout/PlayerScoutVisual");
            CollisionShape2D scoutCollision = GetNode<CollisionShape2D>("PlayerScout/PlayerScoutCollision");
            scoutVisual.Animation = "Square";
            scoutCollision.Shape = new CircleShape2D();
            scoutCollision.Scale = new Vector2(3.2F, 3.2F);

            soulStatus = PlayerSoulStatus.Box;
        }
        else if (newSoulStatus == PlayerSoulStatus.Default)
        {
            soul.CollisionLayer = 34;
            soul.CollisionMask = 34;

            AnimatedSprite2D soulSprite = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulSprite");
            AnimatedSprite2D soulVisual = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulVisual");
            CollisionShape2D soulCollision = GetNode<CollisionShape2D>("PlayerSoul/PlayerSoulCollision");
            AnimatedSprite2D soulEnergy = GetNode<AnimatedSprite2D>("PlayerSoul/PlayerSoulEnergy");
            soulSprite.Animation = "Default";
            soulVisual.Animation = "Default";
            soulCollision.Shape = new CircleShape2D();
            soulCollision.Scale = new Vector2(3.2F, 3.2F);
            soulEnergy.Animation = "Default";

            AnimatedSprite2D scoutVisual = GetNode<AnimatedSprite2D>("PlayerScout/PlayerScoutVisual");
            CollisionShape2D scoutCollision = GetNode<CollisionShape2D>("PlayerScout/PlayerScoutCollision");
            scoutVisual.Animation = "Default";
            scoutCollision.Shape = new CircleShape2D();
            scoutCollision.Scale = new Vector2(3.2F, 3.2F);

            soulStatus = PlayerSoulStatus.Default;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        RestoreEnergy();
        MoveBody(delta);
        SetSoulPosition(delta);
        CheckAttack();

        if (bodyStatus == PlayerBodyStatus.CrouchingCramped)
        {
            beganJumpThisFrame = true;
        }
        else
        {
            beganJumpThisFrame = false;
        }
    }

    public void CheckAttack()
    {
        if (Input.IsActionJustPressed("mouse1"))
        {
            if      (playerMode == PlayerMode.Body)
            {
                DoMouse1Body();
            }
            else // (playerMode == PlayerMode.Soul)
            {
                DoMouse1Soul();
            }
        }

        if (Input.IsActionJustPressed("mouse2"))
        {
            if      (playerMode == PlayerMode.Body)
            {
                DoMouse2Body();
            }
            else // (playerMode == PlayerMode.Soul)
            {
                DoMouse2Soul();
            }
        }

        DoAttack();
    }

    public void DoAttack()
    {
        if (postAttackTimer > 0)
        {
            postAttackTimer--;
        }
        if (postAttackTimer == 0 && preAttackTimer > 0)
        {
            preAttackTimer--;
        }
        if (preAttackTimer == 0 && nextAttack != Attack.None)
        {
            nextAttack = Attack.None;
        }
    }

    public void DecreaseAttackRecovery()
    {

    }

    public void DoMouse1Body()
    {
        if (postAttackTimer == 0)
        {
            //var scene = GD.Load<PackedScene>("res://Projectile.tscn");
            //Projectile instance = (Projectile)scene.Instantiate();
            //instance.Boo();
            //var scene2 = ResourceLoader.Load<PackedScene>("res://Projectile.cs").Instantiate();

            PackedScene Projectiles = GD.Load<PackedScene>("res://projectile.tscn");
            Projectile projectile = (Projectile)Projectiles.Instantiate();
            projectile.GlobalPosition = body.GlobalPosition;
            Node node = GetParent(); //Start
                                     //node.AddChild(projectile);
            body.AddChild(projectile);
            projectile.GlobalPosition = body.GlobalPosition;
            AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");

            projectile.SetData(this, 0, new List<int>() { 0, 1 }, 100, 0, 20, false, 5);
            postAttackTimer = 15;

            if (Input.IsActionPressed("up"))
            {
                projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X, projectile.GlobalPosition.Y - 100);
            }
            else if (bodySprite.FlipH)
            {
                projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X - 100, projectile.GlobalPosition.Y);
            }
            else
            {
                projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X + 100, projectile.GlobalPosition.Y);
            }
        }
    }

    public void DoMouse1Soul()
    {

    }

    public void DoMouse2Body()
    {
        if (energy > 0 && postAttackTimer == 0)
        {
            //var scene = GD.Load<PackedScene>("res://Projectile.tscn");
            //Projectile instance = (Projectile)scene.Instantiate();
            //instance.Boo();
            //var scene2 = ResourceLoader.Load<PackedScene>("res://Projectile.cs").Instantiate();

            PackedScene Projectiles = GD.Load<PackedScene>("res://projectile.tscn");
            Projectile projectile = (Projectile)Projectiles.Instantiate();
            projectile.GlobalPosition = body.GlobalPosition;
            Node node = GetParent(); //Start
                                     //node.AddChild(projectile);
            body.AddChild(projectile);
            projectile.GlobalPosition = body.GlobalPosition;
            AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");

            projectile.SetData(this, 0, new List<int>() { 0, 1 }, 100, 0, 0, true, 5);
            UpdateEnergyOrb(-100);
            postAttackTimer = 35;

            if (Input.IsActionPressed("up"))
            {
                projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X, projectile.GlobalPosition.Y - 100);
            }
            else if (bodySprite.FlipH)
            {
                projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X - 100, projectile.GlobalPosition.Y);
            }
            else
            {
                projectile.GlobalPosition = new Vector2(projectile.GlobalPosition.X + 100, projectile.GlobalPosition.Y);
            }
        }
    }

    public void DoMouse2Soul()
    {

    }

    public override void _Process(double delta)
	{

    }

    public void SetData(float size, double energy, double health, PlayerMode playerMode, bool canGlide)
    {

    }

    public override void _Input(InputEvent @event)
    {

        if (Input.IsActionJustPressed("switch_player_mode"))
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

    public void RestoreEnergy()
    {
        if (body.IsOnFloor() && soulStatus != PlayerSoulStatus.Box)
        {
            UpdateEnergyOrb(400);
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
        }
        else
        {
            soulEnergy.Scale = new Vector2(0, 0);
        }
    }

    public void MoveBody(double delta)
    {
        bool onGroundBefore = body.IsOnFloor();
        bool crouchedBefore = (bodyStatus == PlayerBodyStatus.Crouching || bodyStatus == PlayerBodyStatus.CrouchingCramped);
        Vector2 velocity = body.Velocity;
        MoveBodyVertically(ref velocity);
        MoveBodyHorizontally(ref velocity);
        CheckForTeleport();
        SetPlayerBodyStatus2();
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

    public void CheckForTeleport()
    {
        if (Input.IsActionPressed("teleport"))
        {
            teleportInputTimer++;

            if (energy >= 0 && teleportInputTimer >= 20)
            {

                UpdateEnergyOrb(-2);
            }

            
            if (teleportInputTimer >= 20 && energy >= 50 && soulStatus != PlayerSoulStatus.Box)
            {
                SetSoulStatus(PlayerSoulStatus.Box);
            }
            if (soulStatus == PlayerSoulStatus.Box)
            {
                
                if (energy <= 0)
                {
                    SetSoulStatus(PlayerSoulStatus.Default);
                }
            }
        }
        if (Input.IsActionJustReleased("teleport"))
        {
            if (teleportInputTimer < 10 && energy >= 1)
            {
                Teleport(soul.Position);
                UpdateEnergyOrb(-200);
                UpdateHealthOrb(-5);
            }
            else if (soulStatus == PlayerSoulStatus.Box)
            {
                SetSoulStatus(PlayerSoulStatus.Default);
            }
            teleportInputTimer = 0;
        }
    }

    public void MoveBodyVertically(ref Vector2 velocity)
    {
        GetJumpVelocity(ref velocity);

        if (!body.IsOnFloor())
        {
            float gravity = 25F * size;
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

            if (velocity.Y == 0 && gravityMod == 1)
            {
                gravityMod = 6;
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
            beganJumpThisFrame = true;
            Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
            if (bodyStatus == PlayerBodyStatus.Crouching && energy > 0)
            {
                if (direction.X == 0)
                {
                    velocity.Y += -2100F * size;
                    UpdateEnergyOrb(-200);
                }
                else
                {
                    velocity.Y += -700F * size;
                    UpdateEnergyOrb(-200);
                    bodyStatus = PlayerBodyStatus.Longjumping;
                    longJumpDir = direction.X;
                }
            }
            else
            {
                velocity.Y += -1500F * size;
                //velocity.Y += -1000F * size;
            }
        }
    }

    public float GetGravityMod(Vector2 velocity)
    {
        //This code must be ran after jump velocity has been added

        CheckForSlam();
        if (bodyStatus == PlayerBodyStatus.FastFalling)
        {
            return 28F;
        }
        else if (Input.IsActionPressed("jump") && (velocity.Y <= 0))
        {
            return 1F;
        }
        else if (Input.IsActionPressed("jump") && (velocity.Y >= 0))
        {
            if (canGlide && energy >= 2 && bodyStatus != PlayerBodyStatus.Crouching && bodyStatus != PlayerBodyStatus.CrouchingCramped && bodyStatus != PlayerBodyStatus.Longjumping)
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

    public void CheckForSlam()
    {
        if (Input.IsActionJustPressed("sneak") && !body.IsOnFloor() && bodyStatus != PlayerBodyStatus.CrouchingCramped && fastFallInputTimer != -1 && energy > 0)
        {
            UpdateEnergyOrb(-200);
            bodyStatus = PlayerBodyStatus.FastFalling;
            fastFallInputTimer = -1;
            return;
        }
        if (Input.IsActionJustPressed("sneak") && !body.IsOnFloor() && bodyStatus != PlayerBodyStatus.CrouchingCramped && fastFallInputTimer == -1)
        {
            fastFallInputTimer = 30;
        }
        if (fastFallInputTimer > -1)
        {
            fastFallInputTimer--;
        }
    }

    public void MoveBodyHorizontally(ref Vector2 velocity)
    {
        if (Input.IsActionJustPressed("sneak"))
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
                fastFallInputTimer = 30;
                hasMoved = true;
            }
        }


        if ((bodyStatus == PlayerBodyStatus.Crouching || bodyStatus == PlayerBodyStatus.CrouchingCramped) && !Input.IsActionPressed("sneak"))
        {
            ToggleCrouchMode3();
        }


        float sneakMod = 1F;
        if (bodyStatus == PlayerBodyStatus.Crouching || bodyStatus == PlayerBodyStatus.CrouchingCramped || Input.IsActionPressed("sneak"))
        {
            sneakMod = 0.5F;
        }

        //move_up and move_down do not add velocity, they are there to make sure the code functions
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        if (bodyStatus == PlayerBodyStatus.Longjumping)
        {
            sneakMod = 2.75F;
            if (longJumpDir is not null)
            {
                if (longJumpDir != direction.X)
                {
                    longJumpDir = null;
                    sneakMod = 1F;
                }
            }
        }
        

        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * ((speed * size) * sneakMod);
        }
        else
        {
            velocity.X = Mathf.MoveToward(body.Velocity.X, 0, ((speed * size) * sneakMod));
        }
    }




    public void ToggleCrouchMode3()
    {
        

        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

        bool isCrouched = false;
        bool crouchNow = Input.IsActionPressed("sneak");
        if (bodyCollision.Position.Y == 0F)
        {
            isCrouched = true;
        }

        if (!isCrouched && crouchNow && body.IsOnFloor())
        {
            bodySprite.Position = new Vector2(bodySprite.Position.X, -10F * size);
            bodyCollision.Position = new Vector2(bodySprite.Position.X, 0F);
            bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 0.68F * size);
            body.Position = new Vector2(body.Position.X, body.Position.Y + 30F * size);
            camera.Position = new Vector2(0, -30);
            isCrouched = true;
        }
        else if (isCrouched && !crouchNow && body.IsOnFloor() || isCrouched && !body.IsOnFloor())
        {
            TryToUncrouch3();
        }
        else if (crouchNow)
        {
            //tryingToUncrouch = false;
        }
        SetBodySpriteImage(body.Velocity, bodySprite);
    }

    public void TryToUncrouch3()
    {
        //if (!Input.IsActionPressed("sneak"))
        //{
            //Make the "check for crouch scout collision" part it's own method later
            CharacterBody2D crouchScout = GetNode<CharacterBody2D>("PlayerBody/PlayerBodyCrouch");
            KinematicCollision2D collision = crouchScout.MoveAndCollide(new Vector2(0, 0));
            crouchScout.Position = new Vector2(0, -47);

            if (collision == null)
            {
                AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
                CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
                Camera2D camera = GetNode<Camera2D>("PlayerBody/Camera");

                bodySprite.Position = new Vector2(bodySprite.Position.X, 0F);
                bodyCollision.Position = new Vector2(bodySprite.Position.X, 15F * size);
                bodyCollision.Scale = new Vector2(bodyCollision.Scale.X, 1F * size);
                body.Position = new Vector2(body.Position.X, body.Position.Y - 30F * size);
                camera.Position = new Vector2(0, 0);
            }
        //}
    }


    public void SetBodySprite(Vector2 velocity)
    {
        velocity += body.GlobalPosition;
        AnimatedSprite2D bodySprite = GetNode<AnimatedSprite2D>("PlayerBody/PlayerBodySprite");
        SetBodySpriteDirection(velocity, bodySprite);
        SetBodySpriteImage2(bodySprite);
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
        //if (body.IsOnFloor() && !isCrouched)
        //{
        //    bodySprite.Animation = "Walk";
        //}
        //else if ((body.IsOnFloor() && isCrouched) || (crouchCollisionSinceLastCheck && beganJumpThisFrame))
        //{
        //    bodySprite.Animation = "Crouch";
        //}
        //else if (body.GlobalPosition.Y > velocity.Y)
        //{
        //    bodySprite.Animation = "Jump";
        //}
        //else if (body.GlobalPosition.Y < velocity.Y && prevGravityMod == 1)
        //{
        //    bodySprite.Animation = "Glide";
        //}
        //else if (body.GlobalPosition.Y < velocity.Y && (prevGravityMod == 7 || prevGravityMod == 6))
        //{
        //    bodySprite.Animation = "Fall";
        //}
    }

    public void SetBodySpriteImage2(AnimatedSprite2D bodySprite)
    {
        if (bodyStatus == PlayerBodyStatus.Idle)
        {
            bodySprite.Animation = "Walk";
        }
        else if (bodyStatus == PlayerBodyStatus.Walking)
        {
            bodySprite.Animation = "Walk";
        }
        else if (bodyStatus == PlayerBodyStatus.Crouching)
        {
            bodySprite.Animation = "Crouch";
        }
        else if (bodyStatus == PlayerBodyStatus.CrouchingCramped)
        {
            bodySprite.Animation = "Crouch";
        }
        else if (bodyStatus == PlayerBodyStatus.Jumping)
        {
            bodySprite.Animation = "Jump";
        }
        else if (bodyStatus == PlayerBodyStatus.HighJumping)
        {
            bodySprite.Animation = "Highjump";
        }
        else if (bodyStatus == PlayerBodyStatus.Longjumping)
        {
            bodySprite.Animation = "Longjump";
        }
        else if (bodyStatus == PlayerBodyStatus.Falling)
        {
            bodySprite.Animation = "Fall";
        }
        else if (bodyStatus == PlayerBodyStatus.Gliding)
        {
            bodySprite.Animation = "Glide";
        }
        else // (bodyStatus == PlayerBodyStatus.FastFalling)
        {
            bodySprite.Animation = "Fastfall";
        }
    }

    public void SetPlayerBodyStatus2()
    {
        if (bodyStatus == PlayerBodyStatus.Longjumping && (beganJumpThisFrame || !body.IsOnFloor()) && longJumpDir is not null)
        {
            return;
        }
        longJumpDir = null;

        bool isCrouched = false;
        CollisionShape2D bodyCollision = GetNode<CollisionShape2D>("PlayerBody/PlayerBodyCollision");
        if (bodyCollision.Position.Y == 0F)
        {
            isCrouched = true;
        }
        if (isCrouched)
        {
            CharacterBody2D crouchScout = GetNode<CharacterBody2D>("PlayerBody/PlayerBodyCrouch");
            KinematicCollision2D collision = crouchScout.MoveAndCollide(new Vector2(0, 0));
            crouchScout.Position = new Vector2(0, -47);

            if (collision != null && (beganJumpThisFrame || body.IsOnFloor()))
            {
                bodyStatus = PlayerBodyStatus.CrouchingCramped;
                return;
            }
        }
        if (body.IsOnFloor())
        {
            if (!isCrouched)
            {
                if (body.Velocity.X != 0)
                {
                    bodyStatus = PlayerBodyStatus.Walking;
                }
                else
                {
                    bodyStatus = PlayerBodyStatus.Idle;
                }
            }
            else
            {
                bodyStatus = PlayerBodyStatus.Crouching;
            }
        }
        else
        {
            if (prevGravityMod > 7)
            {
                bodyStatus = PlayerBodyStatus.FastFalling;
            }
            else if (body.Velocity.Y < 0)
            {
                if (body.Velocity.Y < -1500)
                {
                    bodyStatus = PlayerBodyStatus.HighJumping;
                }
                else if (body.Velocity.Y < 0)
                {
                    bodyStatus = PlayerBodyStatus.Jumping;
                }
                
            }
            else
            {
                if (prevGravityMod == 1)
                {
                    bodyStatus = PlayerBodyStatus.Gliding;
                }
                else if (prevGravityMod > 1)
                {
                    bodyStatus = PlayerBodyStatus.Falling;
                }
            }
        }
    }

    public void SetSoulPosition(double delta)
    {
        if (soulStatus != PlayerSoulStatus.Box)
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

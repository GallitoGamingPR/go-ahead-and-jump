# Unity Project: Jumping and Movements Implementation

In this project, we will implement jumping and movements to a robot asset. The following steps outline the process:

1. **Download Input System Package**  
   First, we will download the Input System package from the Package Manager.  
   ![Download Input System](images/gif1.gif)

2. **Restart Project with New Settings**  
   After downloading, we select "Yes" to restart the project with the new settings.  
   ![Restart Project](images/screenshot1.png)

3. **Add Robot Asset and Convert Render Pipeline to URP**  
   Then, we will add the robot asset using the Package Manager and convert the render pipeline to URP.  
   ![Convert to URP](images/screenshot2.png)

4. **Add Player Input Component**  
   We then add the Player Input component to the robot asset.  
   ![Add Player Input Component](images/gif2.gif)

5. **Create Actions for Player Movement**  
   After that, we click "Create Actions" and get the premade actions for player movement.  
   ![Premade Actions](images/screenshot3.png)

6. **Add Additional Actions to Action Map**  
   Then, we add the additional actions to the action map.  
   ![Action Map](images/screenshot4.png)

7. **Add PlayerController Script**  
   Finally, we add a script to the asset called `PlayerController` to handle the movement and jumping actions.
    ![Script](images/screenshot6.png)
9. **Layers**
   We make sure to set a new layer as the ground layer in Edit->Project Settings->Tags & Layers and set the terrain as the ground layer to detect for our jumping.
    ![Layers](images/screenshot5.png)
    ![Ground Layer](images/screenshot7.png)
10. **GroundPoint and ShootPoint**
    We then add two empty objects to our player that will detect the ground and the other will be were our projectiles will shoot out of.
    ![GroundPoint and ShootPoint](images/screenshot8.png)
11. **Testing**
We test the horizontal movement.
 ![Horizontal](images/gif3.gif)
The jumping.
 ![Hjumping](images/gif4.gif)
Then the vertical movement.
 ![Vertical](images/gif5.gif)
Finally the fast horizontal and vertical movement.
 ![Fast](images/gif6.gif)


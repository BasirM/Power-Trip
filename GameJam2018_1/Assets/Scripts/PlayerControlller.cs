 using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerControlller: MonoBehaviour {

    public GameObject player;
    public GameObject top;

    public Material shield;

    public double tankHealth;
    public double playerHealth;
    public double powerLevel;
    public double maxPowerUsage;

    private int attackCoolDown = 0;


    public int rotSpeed;
    public int cannonRot;
    public int moveSpeed;

    // FLASHLIGHT
    bool lightActive;
    public GameObject flashlight;

    public int[] selectedAbilities;
    public GameObject[] bulletPrefabs;
    public bool canMove;

	void Start () {
        flashlight.SetActive(false);
        canMove = true;

        //IMPORTANT ************
        //THIS ONE IS FOR YOU AUTIN BB 00010001 10011000
        /* selectedAbilites is an array where all the default values are set to 0, the point of this is the allocate a single slot to our abilities
         * Example: selectedAbilites[1] is the cannon ability, selectedAbilites[1] can equal the numbers 1,2, or 3. selectedAbilities[1] = 1 means that this is the least 
         * powerful cannon that takes up the least energy, selectedAbilities[1] = 2 mean this is the next power level among the cannon (more damage, more energy) and so on with 
         * selectedAbilities[1] = 3...
         * 
         * now that you should understand the basic concepts here is the array i have created so far
         * selected[1] is the cannon
         * selected[2] is the shield system
         * selected[3] is the life support system
         * selected[4] is the light system
         * the position in the array is based off the numberpad, so selected[1] is 1 on the keypad. selected[2] is 2 on the keypad...
         */
	}
	void Update () {
        
        if(gameObject.transform.position.y > 11)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 11, gameObject.transform.position.z);
        }


        if(powerLevel < 0)
        {
            Debug.Log("Reloading");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (canMove)
        {
            //CoolDown Between each shot fired from tank (reloadTime)
            attackCoolDown = --attackCoolDown >= 0 ? attackCoolDown : 0;



            //Stat used later for calculate the total energy wasted on moving (for GUI use)
            double movementSubtract;


            //The maximum power the tank can use per update (frame)
            maxPowerUsage = 5;


            //Movement

            //Rotates the bottom of the tank
            float rot = (Input.GetAxis("Horizontal") * rotSpeed) * Time.deltaTime;
            player.transform.Rotate(0, rot, 0);

            //Moves the Tank
            float speed = (Input.GetAxis("Vertical") * moveSpeed) * Time.deltaTime;
            player.transform.Translate(0, 0, speed);

            //Rotates the Cannon ontop of the Tank
            float cannon = Input.GetKey("q") ? -1 * cannonRot * Time.deltaTime :
                           Input.GetKey("e") ? 1 * cannonRot * Time.deltaTime : 0f;
            top.transform.Rotate(0, cannon, 0);



            //Simple equation to calcuate energy Usage from moving ( change values)
            movementSubtract = (Mathf.Abs(rot) + Mathf.Abs(speed) + Mathf.Abs(cannon)) / 10;


            //Fire if space or left mouse is clicked and the attackCoolDown is 0
            if (Input.GetKey("space") && attackCoolDown == 0 || Input.GetButton("Fire1") && attackCoolDown == 0)
            {
                //checks if it has power left in its maximumpower reserve to shoot
                if (maxPowerUsage - selectedAbilities[1] > 0)
                {
                    shoot();

                    //subtracts the number at selected[1] from maximumPowerUsage
                    maxPowerUsage -= selectedAbilities[1];
                }
                else
                {
                    //sets the cannon to the lead poweful if there was not enough maximumPower
                    selectedAbilities[1] = 1;
                    shoot();
                }
            }

            if (Input.GetKeyDown("r"))
            {
                CableManager cable = GameObject.Find("LevelManager").GetComponent<CableManager>();

                if (Vector3.Distance(player.transform.position, cable.getLastCircle().transform.position) < 5)
                {
                    cable.endPos = cable.endPos == player.transform ? this.transform : player.transform;
                }

            }

            maxPowerUsage -= movementSubtract;



            //The next foor loop checks if the any of the numbers on the numbpad were clicked, if so they either add a powerlevel to the selected ability or resets it to 0 if its greater than 3
            if (Input.GetKeyDown("1"))
            {
                selectedAbilities[1] = selectedAbilities[1] + 1 <= 3 ? selectedAbilities[1] + 1 : 1;
            }
            if (Input.GetKeyDown("2"))
            {
                selectedAbilities[2] = selectedAbilities[2] + 1 <= 3 ? selectedAbilities[2] + 1 : 0;
            }
            if (Input.GetKeyDown("3"))
            {
                selectedAbilities[3] = selectedAbilities[3] + 1 <= 2 ? selectedAbilities[3] + 1 : 1;
            }
            if (Input.GetKeyDown("4"))
            {
                selectedAbilities[4] = selectedAbilities[4] + 1 <= 1 ? selectedAbilities[4] + 1 : 0;
            }


            //lifeSupport
            //checks is maxpower can hold selected[3] powerlevel *.8
            if (maxPowerUsage - selectedAbilities[3] * .8 > 0)
            {
                maxPowerUsage -= selectedAbilities[3] * .8;
            }
            else
            {
                //if not selectedAbilities[4] gets reset to 0
                selectedAbilities[3] = 0;
            }
            //the whole point of lifesupport is to keep a player alive, this is a simple alorgithmn to see if player loses health
            playerHealth = (75 / tankHealth) > selectedAbilities[3] ? playerHealth - 1 *Time.deltaTime: playerHealth;


            //Shield
            //checks is maxpower can hold selected[2] powerlevel *.7
            if (maxPowerUsage - selectedAbilities[2] * .7 > 0)
            {
                //if can hold the power, change the transparency of the shield surroundign the tank based on powerlevel
                maxPowerUsage -= selectedAbilities[2] * .7;
                Color color = shield.color;
                color.a = selectedAbilities[2] * .09f;
                shield.color = color;
            }
            else
            {
                //if not set the shield to 0
                selectedAbilities[2] = 0;
            }

            //light
            if (maxPowerUsage - selectedAbilities[4] * .5 > 0)
            {
                if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    lightActive = (lightActive) ? false : true;

                    if (lightActive)
                    {
                        maxPowerUsage -= selectedAbilities[4] * .5;
                        flashlight.SetActive(true);
                    }

                    if (!lightActive)
                    {
                        flashlight.SetActive(false);
                    }
                }
            }
            else
            {
                selectedAbilities[4] = 0;
                flashlight.SetActive(false);
            }

            if (lightActive)
            {
                maxPowerUsage -= selectedAbilities[4] * .5;
                flashlight.SetActive(true);
            }


            //subtrracts the maximumpowerused from the total power
            powerLevel -= (5 - maxPowerUsage) * Time.deltaTime;
        }
    }

    public void shoot()
    {
        //reload timer
        attackCoolDown = 100;
        //creates bullet
        BulletManager b = Instantiate(bulletPrefabs[selectedAbilities[1]-1], top.transform.position + (top.transform.forward *4), top.transform.rotation).GetComponent<BulletManager>();
        //bullet script variables
        b.tag = "PlayerBullet";
        b.controller = bulletPrefabs[selectedAbilities[1]-1];
        b.target_Tag = "Enemy";
        b.velocity = top.transform.forward * 40;
        b.damage = selectedAbilities[1] * 25;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            //take damage
            if (other.gameObject.GetComponent<BulletManager>().damage - (selectedAbilities[2] * 10) > 0)
                tankHealth -= (other.gameObject.GetComponent<BulletManager>().damage - (selectedAbilities[2] * 10));
            Destroy(other.gameObject);
        }
    }
}

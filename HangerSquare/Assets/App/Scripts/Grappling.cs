using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
   [SerializeField] private bool isFixedDirection;
   [SerializeField] private Vector2 direction;
   [SerializeField] private TargetJoint2D tj2D;
   [SerializeField] private LineRenderer line;
   [SerializeField] private AudioSource gSound;

   private GameManager gameManager;

   void Start()
   {
      tj2D = GetComponent<TargetJoint2D>();
      gameManager = FindObjectOfType<GameManager>();
      Application.targetFrameRate = 60;
   }

   void Update()
   {
      if (!Application.isMobilePlatform)
      {
         if (Input.GetMouseButtonDown(0)) { GrapplingOn(); }
         if (Input.GetMouseButtonUp(0)) { GrapplingOff(); }
      }
      else
      {
         Touch touch = Input.GetTouch(0);
         if (touch.phase == TouchPhase.Began) { GrapplingOn(); }
         if (touch.phase == TouchPhase.Ended) { GrapplingOff(); }
      }

      line.SetPosition(0, transform.position);
      line.SetPosition(1, tj2D.target);
   }

   public void GrapplingOn()
   {
      Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      Vector2 origin = new Vector2(transform.position.x, transform.position.y + 1f);

      //Vectorun istiqametini tapırıq.
      Vector2 pos = mousePos - origin;

      RaycastHit2D hit = isFixedDirection ? Physics2D.Raycast(origin, direction) : Physics2D.Raycast(origin, pos);

      if (hit.collider != null)
      {
         tj2D.target = hit.point;

         gSound.Play();
         tj2D.enabled = true;
         line.enabled = true;
      }

   }

   public void GrapplingOff()
   {
      tj2D.enabled = false;
      line.enabled = false;
   }

   public void OnCollisionEnter2D(Collision2D collision)
   {
      //gameManager.GameOver();
   }



}
using System.Collections;
using System.Collections.Generic;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using UnityEngine;
using UnityEngine.AI;

public class GoToSchool : CActionBase
{
   public override bool Pre_Perform(CAgent agent)
   {
      AllLocationInfor infor = AllLocationInfor.Instance;
      LocationInformation school = infor.infos.Find(e => e.codeName.Equals("School"));
      if (school != null)
      {
         agent.position3D = school.obj.transform.position;
         return true;
      }

      Debug.LogError("Can not find position of school");
      return false;
   }

   public override bool PerformAction(CAgent agent)
   {
      agent.gameObject.GetComponent<NavMeshAgent>().SetDestination(agent.position3D);
      isActive = true;
      return true;
   }
   
   
}

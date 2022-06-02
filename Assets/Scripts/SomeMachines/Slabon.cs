using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slabon : MonoBehaviour
{
    Rope myRope;
    Slabon myKohai;
    Rigidbody myRig;
    CharacterJoint charJoint;

    public Rigidbody MyRig { get => myRig; }
    public CharacterJoint CharJoint { get => charJoint; }

    public void Initialize()
    {
        myRig = this.gameObject.GetComponent<Rigidbody>();
        charJoint = this.gameObject.GetComponent<CharacterJoint>();
        charJoint.massScale = 10;
    }



    public void SetRope(Rope rope)
    {
        myRope = rope;
    }

    public void SetMyKohai(Slabon myKohai)
    {
        this.myKohai = myKohai;
    }

    public void ImCutted()
    {
        myRope.SlabCutted(this);
        myRig.isKinematic = false;
        charJoint.breakForce = 0;
        // DisconectBody();

        SoundFX.Play_Cut_the_rope();

       // GameObject.Destroy(this.gameObject);
        //myKohai.ImCutted();
    }

    public void DisconectBody()
    {
        //charJoint.breakForce = 0;
    }
}

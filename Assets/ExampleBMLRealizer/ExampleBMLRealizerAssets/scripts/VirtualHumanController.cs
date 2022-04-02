using UnityEngine;
using System.Collections;
using AssetPackage;
using BMLNet;
using SpeechLib;

public class VirtualHumanController : MonoBehaviour {

    SpVoice voice;

    RageBMLNet bml = new RageBMLNet();

	// Use this for initialization
	void Start () {
        // windows text to speech
        voice = new SpVoice();
        voice.Volume = 100;
        voice.Rate = 0;

        // try to parse XML
        bml.ParseFromFile("Assets/ExampleBMLRealizer/ExampleBMLRealizerAssets/BML.xml");

        // add callback for BMLNet
        bml.OnSyncPointCompleted += SyncPointCompleted; // when blm.OnSyncPointCompleted delegate is called, SyncPointCompleted will be called along with other 
                                                        // event handler functions added to the delegate variable. 

    }
	
	// Update is called once per frame
	void Update () {
        // for every update, need to update BMLNet
        bml.Update(Time.deltaTime);
	}

    /// <summary>
    /// callback when there is sync point
    /// this function will be called from BMLNet library
    /// </summary>
    /// <param name="behaviorID"></param>
    /// <param name="eventName"></param>
    /// 
    // Note    bmlNet.OnSyncPointCompleted(parentBlock.id, eventName) in Update() of BMLSyncPoint.cs will call
    // the following event handler.
    void SyncPointCompleted(string behaviorID, string eventName)
    {
        BMLBlock block = bml.GetBehaviorFromId(behaviorID);

        // get the character that will performs
        GameObject character = GameObject.Find(block.getCharacterId());

        Debug.Log(block.getCharacterId() + " " + behaviorID + " " + eventName);

        // cannot find the character
        if (character == null)
            return;

        if (block is BMLFace)
        {
            BMLFace face = (BMLFace)block;

        }
        else if (block is BMLFaceFacs)
        {
            BMLFaceFacs face = (BMLFaceFacs)block;

        }
        else if (block is BMLFaceLexeme)
        {
            BMLFaceLexeme face = (BMLFaceLexeme)block;

        }
        else if (block is BMLFaceShift)
        {
            BMLFaceShift face = (BMLFaceShift)block;

        }

        else if (block is BMLGaze)
        {
            // get the gaze behaviour
            BMLGaze gaze = (BMLGaze)block;

            // get the target that need to be gazed
            GameObject target = GameObject.Find(gaze.target);
            if (target != null)
            {
                HeadLookController controller = character.GetComponent<HeadLookController>();
                if (controller != null)
                {
                    // setting parameter for behaviour
                    controller.targetNode = target.transform;

                    // setting parameter for callback
                    controller.SetBMLNetParam(block.getCharacterId(), behaviorID, eventName);

                    // add callback
                    controller.OnBehaviourCompleted += SetTrigger; // SetTrigger() will be called when controller.OnBehaviourCompleted(,,) will be called
                }
            }

        }
        else if (block is BMLGazeShift)
        {
            BMLGazeShift gazeShift = (BMLGazeShift)block;

        }

        else if (block is BMLGesture)
        {
            BMLGesture gesture = (BMLGesture)block;            

            Animator animator = character.GetComponentInChildren<Animator>();

            if (gesture.id == "gesture1")
                animator.SetTrigger("Show");


        }
        else if (block is BMLPointing)
        {
            BMLPointing pointing = (BMLPointing)block;

        }

        else if (block is BMLHead)
        {
            BMLHead head = (BMLHead)block;


        }
        else if (block is BMLHeadDirectionShift)
        {
            BMLHeadDirectionShift headDirectionShift = (BMLHeadDirectionShift)block;

        }

        else if (block is BMLLocomotion)
        {
            BMLLocomotion locomotion = (BMLLocomotion)block;

        }

        else if (block is BMLPosture)
        {
            BMLPosture posture = (BMLPosture)block;

        }
        else if (block is BMLPostureShift)
        {
            BMLPostureShift postureShift = (BMLPostureShift)block;

        }
        else if (block is BMLStance)
        {
            BMLStance stance = (BMLStance)block;

        }
        else if (block is BMLPose)
        {
            BMLPose pose = (BMLPose)block;

        }

        else if (block is BMLSpeech)
        {
            BMLSpeech speech = (BMLSpeech)block;

            voice.Speak(speech.text, SpeechVoiceSpeakFlags.SVSFlagsAsync);

        }
    }

    /// <summary>
    /// this function helper will be called from every sync point that need to be triggered from Unity.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="characterId"></param>
    /// <param name="behaviorId"></param>
    /// <param name="eventName"></param>
    public void SetTrigger(BMLNetBehaviour obj, string characterId, string behaviorId, string eventName)
    {
        // remove the callback
        obj.OnBehaviourCompleted -= SetTrigger;

        // trigger BMLNet callback
        bml.TriggerSyncPoint(behaviorId, eventName);

        //  public void TriggerSyncPoint(string id, string eventName) // BMLNet.TriggerSyncPoint( , )
        // {
        //     if (scheduledBlocks.ContainsKey(id))
        //     {
        //         if (scheduledBlocks[id].syncPoints.ContainsKey(eventName) == false)
        //         {
        //             // create a new sync point
        //             scheduledBlocks[id].syncPoints.Add(eventName, new BMLSyncPoint(scheduledBlocks[id], eventName, ""));
        //         }

        //         // trigger sync point
        //         scheduledBlocks[id].syncPoints[eventName].TriggerSyncPoint(); // BMLSyncPoint.TriggerSyncPoint() for a given block id and sync point with eventName
        //     }
        // }

    } // public void SetTrigger

}

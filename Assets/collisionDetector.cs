using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class collisionDetector : MonoBehaviour
{
    public bool EnteredTrigger = false;
    MqttClient client = new MqttClient("localhost");




    // Start is called before the first frame update
    void Start()
    {
        client.Connect(Guid.NewGuid().ToString());
        string cliendit = Guid.NewGuid().ToString();
        client.Connect(cliendit);
        //client.MqttMsgPublishReceived += client_MqttMsgrecieved;
        //client.Subscribe(new string[] { "turnon/" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    private static void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
    {
    }
    private static void client_MqttMsgrecieved(object sender, MqttMsgPublishEventArgs e)
    {
        var msg = System.Text.Encoding.UTF8.GetString(e.Message);
       
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void TestPublish()
    {
        client.MqttMsgPublished += client_MqttMsgPublished;
        ushort msgId = client.Publish("turnon/", Encoding.UTF8.GetBytes("1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published => on");

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            EnteredTrigger = true;
            Destroy(other.gameObject);
            TestPublish();

        }

    }
}

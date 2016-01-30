using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

// OAuthToken can be found here: https://twitchapps.com/tmi/

public class TwitchIRC : MonoBehaviour
{
	public string Hostname = "irc.twitch.tv";
	public int Port = 6667;
	public string Username;
	public string StreamName;
	public string OAuthToken;

	private const int m_ReadBufferLength = 1024;
	private const string m_EndPacketString = "\r\n";
	private TcpClient m_TcpClient;
	private Encoding m_Encoding;
	private byte[] m_ReadBuffer;
	private string m_PartialMessage;
	private Queue<string> m_MessageQueue;
	private bool m_IsWriting;
	private delegate void CommandHandler(MessageIRC message);
	private Dictionary<string, CommandHandler> m_CommandHandlers;

	public void Start()
	{
		m_Encoding = Encoding.UTF8;
		m_ReadBuffer = new byte[m_ReadBufferLength];
		m_MessageQueue = new Queue<string>();
		m_IsWriting = false;
		m_PartialMessage = "";
		m_CommandHandlers = new Dictionary<string, CommandHandler>();
		m_CommandHandlers["ping"] = HandlePingCommand;
		m_CommandHandlers["privmsg"] = HandlePrivateMessage;

		m_TcpClient = new TcpClient();
		m_TcpClient.BeginConnect(Hostname, Port, new AsyncCallback(ConnectComplete), null);
	}

	public void OnDestroy()
	{
		Disconnect();
	}

	public void Update()
	{
		if (!m_IsWriting && m_MessageQueue.Count > 0)
		{
			string message = m_MessageQueue.Dequeue();
			SendRawMessage(message);
		}
	}

	private void ConnectComplete(IAsyncResult result)
	{
		m_TcpClient.EndConnect(result);
		if (!m_TcpClient.Connected)
		{
			Debug.LogError("Could not connect to TwitchTV");
			Disconnect(false);
			return;
		}
		Debug.Log("Connected to TwitchTV");

		m_TcpClient.GetStream().BeginRead(m_ReadBuffer, 0, m_ReadBufferLength, new AsyncCallback(HandleData), null);

		if (!string.IsNullOrEmpty(OAuthToken))
		{
			SendRawMessage("PASS {0}", OAuthToken);
		}
		SendRawMessage("NICK {0}", Username.ToLower());
		SendRawMessage("JOIN #{0}", StreamName.ToLower());
	}

	private void HandleData(IAsyncResult result)
	{
		if (m_TcpClient == null)
		{
			return;
		}

		int length = m_TcpClient.GetStream().EndRead(result);
		if (length == 0)
		{
			Debug.Log("Connection dropped by server");
			Disconnect(false);
			return;
		}

		int readOffset = 0;
		while (length > 0)
		{
			int messageLength = Array.IndexOf(m_ReadBuffer, (byte)'\n', readOffset, length);
			messageLength++;
			int bytesToRead = messageLength;
			if (bytesToRead == 0)
			{
				bytesToRead = m_ReadBufferLength;
			}
			bytesToRead -= readOffset;

			StringBuilder rawMessage = new StringBuilder(m_PartialMessage);
			m_PartialMessage = "";
			rawMessage.Append(m_Encoding.GetString(m_ReadBuffer, readOffset, bytesToRead));
			if (rawMessage.Length > 0)
			{
				if (messageLength == 0)
				{
					// raw message is not complete
					m_PartialMessage = rawMessage.ToString();
					break;
				}
				else
				{
					// raw message complete
					MessageIRC ircMessage = new MessageIRC(rawMessage.ToString());
					string commandToLower = ircMessage.Command.ToLower();
					if (m_CommandHandlers.ContainsKey(commandToLower))
					{
						m_CommandHandlers[commandToLower](ircMessage);
					}
				}
			}
			else
			{
				Debug.LogError("Raw message is empty");
			}

			length -= bytesToRead;
			readOffset += bytesToRead;
		}

		m_TcpClient.GetStream().BeginRead(m_ReadBuffer, 0, m_ReadBufferLength, new AsyncCallback(HandleData), null);
	}

	public void SendRawMessage(string message, params object[] format)
	{
		if (m_TcpClient == null)
		{
			return;
		}

		message = string.Concat(string.Format(message, format), "\r\n");
		byte[] data = m_Encoding.GetBytes(message);
		if (!m_IsWriting)
		{
			m_IsWriting = true;
			m_TcpClient.GetStream().BeginWrite(data, 0, data.Length, (IAsyncResult result) =>
			{
				m_TcpClient.GetStream().EndWrite(result);
				m_IsWriting = false;
			}, null);
		}
		else
		{
			m_MessageQueue.Enqueue(message);
		}
	}

	public void Disconnect(bool sendDisconnectMessage = true)
	{
		if (sendDisconnectMessage)
		{
			SendRawMessage("QUIT");
		}

		if (m_TcpClient != null)
		{
			m_TcpClient.Client.BeginDisconnect(true, (IAsyncResult result) =>
	        {
				m_TcpClient.Client.EndDisconnect(result);
				m_TcpClient.GetStream().Close();
				m_TcpClient.Close();
			}, null);
		}
	}

	public delegate void MessageRecievedHandler(MessageIRC message);
	public event MessageRecievedHandler MessageRecievedEvent;
	internal void OnMessageRecieved(MessageIRC message)
	{
		if (MessageRecievedEvent != null) MessageRecievedEvent(message);
	}

#region Command handlers
	void HandlePingCommand(MessageIRC message)
	{
		Debug.Log("got pinged: " + message.Parameters[0]);
		SendRawMessage("PONG :{0}", message.Parameters[0]);
	}

	void HandlePrivateMessage(MessageIRC message)
	{
#if DEBUG
		string log = string.Format("Message from {0}: {1}", message.Parameters[0], message.Parameters[1]);
		Debug.Log(log);
#endif
		OnMessageRecieved(message);
	}
#endregion Command handlers
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class dialogueManager : MonoBehaviour
{
    
    [SerializeField] GameObject countdownBar;
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] GameObject dialogueImageObject;
    [SerializeField] GameObject secondDialogueImageObject;
    [SerializeField] TextMeshProUGUI secondTextComponent;
    [SerializeField] TextMeshProUGUI firstButton, secondButton, thirdButton, fourthButton;
    [SerializeField] GameObject firstButtonG, secondButtonG, thirdButtonG, fourthButtonG;
    [SerializeField] TextAsset textFile;
    [SerializeField] List<Sprite> images = new List<Sprite>();
    [SerializeField] Image backgroundImage;
    [SerializeField] GameObject returnButton;
    public AK.Wwise.RTPC Anger;
    public AK.Wwise.RTPC Empathy;
    public AK.Wwise.RTPC Loyalty_Ambition;

    private int dialogueNumber = 0;
    private int nextElement = 0;
    public int critical, grief, leadership, loyalty;
    private string[] lineByLine = new String[256];
    private string firstChoice;
    private string secondChoice;
    private string thirdChoice;
    private string fourthChoice;
    private bool isChoice = false;
    private bool nextString = false;
    private int timeGone = 0;
    private Sprite currentKing;
    private Sprite currentQuestion;
    private bool ending = false;
    private int stage = 0;
    private GameObject wwiseObject;
    private bool earlyEnding;
    private Vector2 dialogueBoxSize;
    private Color dialogueBoxColor;
    private int messageNumber;

    private void Start()
    {
        currentKing = images[0];
        currentQuestion = images[1];
        firstButtonG.SetActive(false);
        secondButtonG.SetActive(false);
        thirdButtonG.SetActive(false);
        fourthButtonG.SetActive(false);
        SplitText();
        NextText();
        StartCoroutine(time());
        wwiseObject = GameObject.FindGameObjectWithTag("Wwise Global");
    }

    private void Update()
    {
        if(firstButtonG.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                Choice(1);
            }
        }
        if (secondButtonG.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Choice(2);
            }
        }
        if (thirdButtonG.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Choice(3);
            }
        }
        if (fourthButtonG.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Choice(4);
            }
        }
    }

    public void SplitText()
    {
        int middleMan = 0;
        string fullText = textFile.text;
        int i = 0;
        int s = 0;
        foreach (char letter in fullText)
        {
            i++;
            if (letter == '\n')
            {
                lineByLine[s] = fullText.Slice(middleMan, i);
                middleMan = i;
                s++;
            }
            
        }
    }
    public void NextText()
    {
        textComponent.text = "";
        firstButton.text = "";
        secondButton.text = "";
        thirdButton.text = "";
        fourthButton.text = "";
        isChoice = false;
        bool isRight = false;
        bool isDone = false;
        
        if (dialogueNumber > 38 && stage == 0)
        {
            currentQuestion = images[2];
            stage++;
        }
        else if (dialogueNumber > 84 && stage == 1)
        {
            currentQuestion = images[4];
            currentKing = images[3];
            stage++;
        }
        else if (dialogueNumber > 126 && stage == 2)
        {
            currentQuestion = images[6];
            currentKing = images[5];
            stage++;
        }
        else if (dialogueNumber > 172 && stage == 3)
        {
            currentQuestion = images[8];
            currentKing = images[7];
            stage++;
        }
        else if (dialogueNumber > 220 && stage == 4)
        {
            currentQuestion = images[10];
            currentKing = images[9];
        }

        for (int i = dialogueNumber; !isDone; i++)
        {
            Debug.Log(lineByLine.Length);
            if (lineByLine[i][0] == 'S')
            {
                if (earlyEnding == true)
                {
                    AkSoundEngine.PostEvent("KingDeath_Early", wwiseObject);
                }
                dialogueImageObject.GetComponent<RectTransform>().sizeDelta = dialogueBoxSize;
                dialogueImageObject.GetComponent<Image>().color = dialogueBoxColor;
                returnButton.SetActive(true);
                DisplayEnding();
                isDone = true;
                isChoice = true;
            }
            else if (lineByLine[i][0] == 'E')
            {
                earlyEnding = true;
                ending = true;
                countdownBar.SetActive(false);
                backgroundImage.sprite = images[11];
                textComponent.color = Color.white;
                dialogueBoxSize = dialogueImageObject.GetComponent<RectTransform>().sizeDelta;
                dialogueBoxColor = dialogueImageObject.GetComponent<Image>().color;
                dialogueImageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(900, 600);
                dialogueImageObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }
            else if (lineByLine[i][0] == '#')
            {
                if (lineByLine[i][1] - '0' == nextElement)
                {
                    //right thingy
                    isChoice = false;
                    isRight = true;
                    if (!ending)
                    {
                        backgroundImage.sprite = currentKing;
                    }
                    
                    if (lineByLine[i].Length > 4)
                    {
                        //backgroundImage.sprite = images[lineByLine[i][2] - '0'];
                    }
                }
                else
                {
                    //continue
                }

            }
            else if (lineByLine[i][0] == '%')
            {
                if (lineByLine[i][1] - '0' == nextElement)
                {
                    //right thingy
                    isChoice = true;
                    isRight = true;
                    if (!ending)
                    {
                        backgroundImage.sprite = currentQuestion;
                    }
                    
                    if (lineByLine[i].Length > 4)
                    {
                        //backgroundImage.sprite = images[lineByLine[i][2] - '0'];
                    }
                }
            }
            else if (lineByLine[i][0] == '"' && isRight)
            {
                textComponent.text = lineByLine[i].Slice(1, lineByLine[i].Length - 1);
                if (lineByLine[i+1][0] == '"')
                {
                    isDone = true;
                    nextString = true;
                    
                }
            }
            else if (lineByLine[i][0] == '"' && nextString)
            {
               
                textComponent.text = lineByLine[i].Slice(1, lineByLine[i].Length - 1);
                if (lineByLine[i + 1][0] == '"')
                {
                    isDone = true;
                    nextString = true;
                }
                else
                {
                    isRight = true;
                    nextString = false;
                }

            }
            else if ((lineByLine[i][0] - '0' == 1 || lineByLine[i][0] - '0' == 2 || lineByLine[i][0] - '0' == 3 || lineByLine[i][0] - '0' == 4 || lineByLine[i][0] - '0' == 5 || lineByLine[i][0] - '0' == 0) && isRight)
            {
                if (isChoice)
                {
                    //show choices and buttons
                    if (lineByLine[i] != "")
                    {
                        firstButtonG.SetActive(true);
                        firstChoice = lineByLine[i];
                        firstButton.text = "1) " + lineByLine[i].Slice(6, lineByLine[i].Length - 1);
                        if (lineByLine[i+1] != "")
                        {
                            secondButtonG.SetActive(true);
                            secondChoice = lineByLine[i + 1];
                            secondButton.text = "2) " + lineByLine[i + 1].Slice(6, lineByLine[i + 1].Length - 1);
                            if (lineByLine[i + 2][0] - '0' >= 0 && lineByLine[i + 2][0] - '0' <= 9)
                            {
                                thirdButtonG.SetActive(true);
                                thirdChoice = lineByLine[i + 2];
                                thirdButton.text = "3) " + lineByLine[i + 2].Slice(6, lineByLine[i + 2].Length - 1);
                                if (lineByLine[i + 3][0] - '0' >= 0 && lineByLine[i + 3][0] - '0' <= 9)
                                {
                                    fourthButtonG.SetActive(true);
                                    fourthChoice = lineByLine[i + 3];
                                    fourthButton.text = "4) " + lineByLine[i + 3].Slice(6, lineByLine[i + 3].Length - 1);
                                    i++;
                                }
                                i++;
                            }
                            i++;
                        }
                        i++;
                    }
                    isDone = true;
                    //isChoice = false;
                } 
                else
                {
                    nextElement = lineByLine[i][0] - '0';
                    isRight = false;
                    isDone = true;
                    i++;
                }
                
            }
            /*else
            {
                isDone = true;
            }*/

            dialogueNumber = i;
        }
        dialogueNumber++;
    }

    public void Choice(int choiceNumber)
    {
        if (choiceNumber == 1)
        {
            //Set the next element
            nextElement = firstChoice[0] - '0';
            //First statpoint
            if (firstChoice[1] - '0' == 1)
            {
                critical++;
            }
            else if (firstChoice[1] - '0' == 2)
            {
                grief++;
            }
            else if (firstChoice[1] - '0' == 3)
            {
                leadership++;
            }
            else if (firstChoice[1] - '0' == 4)
            {
                loyalty++;
            }
            //Second statpoint
            if (firstChoice[2] - '0' == 1)
            {
                critical++;
            }
            else if (firstChoice[2] - '0' == 2)
            {
                grief++;
            }
            else if (firstChoice[2] - '0' == 3)
            {
                leadership++;
            }
            else if (firstChoice[2] - '0' == 4)
            {
                loyalty++;
            }
        } 
        else if (choiceNumber == 2)
        {
            nextElement = secondChoice[0] - '0';
            //First statpoint
            if (secondChoice[1] - '0' == 1)
            {
                critical++;
            }
            else if (secondChoice[1] - '0' == 2)
            {
                grief++;
            }
            else if (secondChoice[1] - '0' == 3)
            {
                leadership++;
            }
            else if (secondChoice[1] - '0' == 4)
            {
                loyalty++;
            }
            //Second statpoint
            if (secondChoice[2] - '0' == 1)
            {
                critical++;
            }
            else if (secondChoice[2] - '0' == 2)
            {
                grief++;
            }
            else if (secondChoice[2] - '0' == 3)
            {
                leadership++;
            }
            else if (secondChoice[2] - '0' == 4)
            {
                loyalty++;
            }
        }
        else if (choiceNumber == 3)
        {
            nextElement = thirdChoice[0] - '0';
            //First statpoint
            if (thirdChoice[1] - '0' == 1)
            {
                critical++;
            }
            else if (thirdChoice[1] - '0' == 2)
            {
                grief++;
            }
            else if (thirdChoice[1] - '0' == 3)
            {
                leadership++;
            }
            else if (thirdChoice[1] - '0' == 4)
            {
                loyalty++;
            }
            //Second statpoint
            if (thirdChoice[2] - '0' == 1)
            {
                critical++;
            }
            else if (thirdChoice[2] - '0' == 2)
            {
                grief++;
            }
            else if (thirdChoice[2] - '0' == 3)
            {
                leadership++;
            }
            else if (thirdChoice[2] - '0' == 4)
            {
                loyalty++;
            }
        }
        else if (choiceNumber == 4)
        {
            nextElement = fourthChoice[0] - '0';
            //First statpoint
            if (fourthChoice[1] - '0' == 1)
            {
                critical++;
            }
            else if (fourthChoice[1] - '0' == 2)
            {
                grief++;
            }
            else if (fourthChoice[1] - '0' == 3)
            {
                leadership++;
            }
            else if (fourthChoice[1] - '0' == 4)
            {
                loyalty++;
            }
            //Second statpoint
            if (fourthChoice[2] - '0' == 1)
            {
                critical++;
            }
            else if (fourthChoice[2] - '0' == 2)
            {
                grief++;
            }
            else if (fourthChoice[2] - '0' == 3)
            {
                leadership++;
            }
            else if (fourthChoice[2] - '0' == 4)
            {
                loyalty++;
            }
        }
        Anger.SetGlobalValue(critical * 15);
        Empathy.SetGlobalValue(grief * 15);
        Loyalty_Ambition.SetGlobalValue((leadership - loyalty) * 10);

        firstButtonG.SetActive(false);
        secondButtonG.SetActive(false);
        thirdButtonG.SetActive(false);
        fourthButtonG.SetActive(false);
        NextText();
    }

    public void AlmostNext()
    {
        if (!isChoice)
        {
            NextText();
        }
    }

    IEnumerator time()
    {
        while (true)
        {
            timeCount();
            yield return new WaitForSeconds(1);
        }
    }

    void timeCount()
    {
        
        timeGone += 1;
        if (timeGone == 10 && stage == 0)
        {
            currentQuestion = images[2];
            stage++;
        } else if (timeGone == 20 && stage == 1)
        {
            currentQuestion = images[4];
            currentKing = images[3];
            stage++;
        }
        else if (timeGone == 30 && stage == 2)
        {
            currentQuestion = images[6];
            currentKing = images[5];
            stage++;
        }
        else if (timeGone == 40 && stage == 3)
        {
            currentQuestion = images[8];
            currentKing = images[7];
            stage++;
        }
        else if (timeGone == 50 && stage == 4)
        {
            currentQuestion = images[10];
            currentKing = images[9];
            stage++;
        } else if (timeGone >= 60 && stage == 5)
        {
            stage++;
            TimerEnd();
        }
    }

    public void TimerEnd()
    {
        dialogueNumber = 247;
        nextElement = 1;
        NextText();
    }

    public void ReturnToMenu()
    {
        AkSoundEngine.PostEvent("Button_BackToMenu", wwiseObject);
        //AkSoundEngine.PostEvent("Button_Click", wwiseObject);
        
        //SceneManager.LoadScene(0);
    }

    public void DisplayEnding()
    {
        if (messageNumber == 0)
        {
            secondDialogueImageObject.SetActive(true);
            dialogueImageObject.SetActive(false);
        }
        if (leadership > 4 && critical < 4 && loyalty < 4 && grief < 4)
        {

            string[] messages = { "The King is inspired and gives you the throne. And yet, wishful believing and realistic outlook are different things.", "You turn the world into chaos because you were too focused on promoting yourself rather than learning in what state the king was leaving the kingdom in.", "you are young and naive. The kingdom is yours but it soon burns and your reign is ended." };
            if (messageNumber < messages.Length)
            {
                secondTextComponent.text = messages[messageNumber];
            }
            
            if (messageNumber == 0)
            {
                backgroundImage.sprite = images[12];
            }

            //textComponent.text = "The King is inspired and gives you the throne. And yet, wishful believing and realistic outlook are different things. You turn the world into chaos because you were too focused on promoting yourself rather than learning in what state the king was leaving the kingdom in, for you are young and naive. The kingdom is yours but it soon burns and your reign is ended.";
        }
        else if (leadership > 4 && critical > 4)
        {
            string[] messages = { "You're good to critisize, but can you handle being in his shoes? The King has someone in mind for ruling, but he is so furious with you", "He wishes to teach you one final lesson before death, and makes you ruler but for a week. Minus any advisors, no round table, no ministers, no court and not even servants.", "The people are demanding the castle for action, but alone you have no idea what to do now and end up trying to run everything at one on your own. It's too much work, you don't even sleep. After your one week of humiliation and hate from the people, the royal advisors remove you and place the better candidate to rule."};
            if (messageNumber == 0)
            {
                backgroundImage.sprite = images[12];
            }
            if (messageNumber < messages.Length)
            {
                secondTextComponent.text = messages[messageNumber];
            }
            //textComponent.text = "You're good to critisize, but can you handle being in his shoes? The King has someone in mind for ruling, but he is so furious with you he wishes to teach you one final lesson before death, and makes you ruler but for a week. Minus any advisors, no round table, no ministers, no court and not even servants. The people are demanding the castle for action, but alone you have no idea what to do now and end up trying to run everything at one on your own. It's too much work, you don't even sleep. After your one week of humiliation and hate from the people, the royal advisors remove you and place the better candidate to rule.";
        }
        else if (grief > 5 && leadership < 5 && loyalty < 5 && critical < 5)
        {
            string[] messages = { "You're a loving child who spends her last moments with the King showing your love and affection. The King is afraid you'll be too easily manipulated.", "So, he orders as his dying wish for his loyalists to keep you safe while the Queen rules. You make it your mission to find the person who did this, and defend the new ruler with all your heart. You eventually find the traitor and bring justice, and become the new Queen's protector, keeping the cycle of protection and love going." };
            if (messageNumber < messages.Length)
            {
                secondTextComponent.text = messages[messageNumber];
            }
            if (messageNumber == 0)
            {
                backgroundImage.sprite = images[12];
            }
            //textComponent.text = "You're a loving child who spends her last moments with the King showing your love and affection. The King is afraid you'll be too easily manipulated. Instead, he orders as his dying wish for his loyalists to keep you safe while the Queen rules. You make it your mission to find the person who did this, and defend the new ruler with all your heart. You eventually find the traitor and bring justice, and become the new Queen's protector, keeping the cycle of protection and love going.";
        }
        else if(grief > 4 && leadership < 4 && loyalty > 4 && critical < 4)
        {
            string[] messages = { "You could never rule the way he did, so even though the king chooses you, you decline. You can't live in a kingdom where he isn't King.", "You could have been the best king, perhaps the best candidate indeed, but the legacy is wasted, ironically, due to your devotion. You kill yourself too to follow your king." };
            if (messageNumber < messages.Length)
            {
                secondTextComponent.text = messages[messageNumber];
            }
            if (messageNumber == 0)
            {
                backgroundImage.sprite = images[12];
            }
            //textComponent.text = "You could never rule the way he did, so even though the king chooses you, you decline. You can't live in a kingdom where he isn't King. You could have been the best king, perhaps the best candidate indeed, but the legacy is wasted, ironically, due to your devotion. You kill yourself too to follow your king.";
        }
        else if (grief < 4 && leadership < 4 && loyalty < 4 && critical > 4)
        {
            string[] messages = { "So little time, and you wasted it all. Not only that, but your rage is uncontained. People know you are actively looking to avenge the king, and that you won't stop at anything to gain your hands on the throne.", "The commotion results in days of anarchy and slowed ministry decisions on what to do next. You are backstabbed and, in turn, poisoned. Rage and suffering is never the answer. The fate of the kingdom is now unknown." };
            if (messageNumber < messages.Length)
            {
                secondTextComponent.text = messages[messageNumber];
            }
            //textComponent.text = "So little time, and you wasted it all. Not only that, but your rage is uncontained. People know you are actively looking to avenge the king, and that you won't stop at anything to gain your hands on the throne. The commotion results in days of anarchy and slowed ministry decisions on what to do next. You are backstabbed and, in turn, poisoned. Rage and suffering is never the answer. The fate of the kingdom is now unknown.";
            if (messageNumber == 0)
            {
                backgroundImage.sprite = images[12];
            }
        }
        else if (leadership > 2 && loyalty > 2 && critical > 2)
        {
            string[] messages = { "You showed some backbone there, not afraid to criticize the king. And yet, also acknowldge that it is not easy to be king. You showed humility and teamwork by saying that the king was never meant to do it all alone.", "You also showed ambition, saying that at the end of it all, the king makes or break a country and is the one to take all the blows, but also fame, so victory for the land is all that matters at the end.", "But, all due respect, the king served his people well, and you will pay him respect with a yearly memorial. You don't insist you can do the job any better. king doesn't make you heir apparent just yet.", "He instructs his loyalists to give you royal trainings while the ministry and advisors semi-rule with you. He has faith, and hopes that belief will pay off. It does. One year (plus one day) after grieving, you become king. But even when the table decided you are ready, you keep the ruling as is, semi-ruling together." };
            if (messageNumber < messages.Length)
            {
                secondTextComponent.text = messages[messageNumber];
            }
            if (messageNumber == 0)
            {
                backgroundImage.sprite = images[13];
            }
            //textComponent.text = "You showed some backbone there, not afraid to criticize the king. And yet, also acknowldge that it is not easy to be king. You showed humility and teamwork by saying that the king was never meant to do it all alone. You also showed ambition, saying that at the end of it all, the king makes or break a country and is the one to take all the blows, but also fame, so victory for the land is all that matters at the end.  But, all due respect, the king served his people well, and you will pay him respect with a yearly memorial. You don't insist you can do the job any better. king doesn't make you heir apparent just yet. He instructs his loyalists to give you royal trainings while the ministry and advisors semi-rule with you. He has faith, and hopes that belief will pay off. It does. One year (plus one day) after grieving, you become king. But even when the table decided you are ready, you keep the ruling as is, semi-ruling together. ";
        }
        else
        {
            string[] messages = { "You fail to leave much of an impression on the king. He has decided that you shall not inherit the throne and will continue your life as a lady of the court." };
            if (messageNumber < messages.Length)
            {
                secondTextComponent.text = messages[messageNumber];
            }
            if (messageNumber == 0)
            {
                backgroundImage.sprite = images[12];
            }
            //textComponent.text = "You fail to leave much of an impression on the king. He has decided that you shall not inherit the throne and will continue your life as a lady of the court.";
        }
        messageNumber++;
    }
}

public static class Extensions
{
    /// <summary>
    /// Get the string slice between the two indexes.
    /// Inclusive for start index, exclusive for end index.
    /// </summary>
    public static string Slice(this string source, int start, int end)
    {
        if (end < 0) // Keep this for negative end support
        {
            end = source.Length + end;
        }
        int len = end - start;               // Calculate length
        return source.Substring(start, len); // Return Substring of length
    }
}

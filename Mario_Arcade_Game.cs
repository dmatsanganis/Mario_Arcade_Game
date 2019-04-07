using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMPLib;

namespace WindowsFormsApp9
{
    public partial class Mario_Arcade_Game : Form
    {
        int score = 0;                                                                                     //Score,an int-type variable, equals to zero at the start of the game.
        int counter = 60;                                                                                  //Timer,an int-type variable, is set on 1 minute(60seconds).       
        int top_scorer1 = 0;                                                                               //An int-type variable for the highscore on easy level difficulty.
        int top_scorer2 = 0;                                                                               //An int-type variable for the highscore on professional level difficulty.                                        
        bool easy = false;                                                                                 //A bool-type variable which shows when difficulty level is set on easy.                                                                   
        bool professional = false;                                                                         //A bool-type variable which shows when difficulty level is set on professional.                                                                                                                      
        bool scorekiller = true;                                                                           //A bool-type variable which stops the timers and freezes the score.                                                                                
        bool start = false;                                                                                //A bool-type variable which shows when the start button is clicked and the game is on.         

        Random r = new Random();                                                                           //A random-type variable, r.        
        WindowsMediaPlayer music_player_Intro = new WindowsMediaPlayer();                                  //A WindowsMediaPlayer-type variable which is responsible for the song at the intro screen,before the game starts.                            
        WindowsMediaPlayer music_player1 = new WindowsMediaPlayer();                                       //A WindowsMediaPlayer-type variable which is responsible for the song while the game is on.                             
        WindowsMediaPlayer soundeffects = new WindowsMediaPlayer();                                        //A WindowsMediaPlayer-type variable which is responsible for the sound effects when Mario(pictureBox1) is clicked.                                    

        public Mario_Arcade_Game()
            {
                InitializeComponent();                                                                     //Initialize the Form.                                                         
                music_player_Intro.URL = "Intro.mp3";                                                      //Load the Into song "Intro.mp3".                                                                             
            }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileInfo f1 = new FileInfo("top_scorers1.txt");                                                //A fileInfo-type variable, f1 (top_scorers1.txt).
            if (f1.Length != 0)                                                                            //If file f1 is not empty.                         
            {
                List<string> names = new List<string>();                                                   //A list of strings, name.                                             
                List<int> scores = new List<int>();                                                        //A list of ints, scores.                              
                richTextBox2.LoadFile("top_scorers1.txt", RichTextBoxStreamType.PlainText);                //richTextBox2 loads f1("top_scorers1.txt").
                 
                foreach (string s in richTextBox2.Lines)                                                   //Foreach string in richTextBox2.   
                {
                    string[] r_textbox2 = s.Split(new string[] { "  Score : " }, StringSplitOptions.None); //Variables Splited by "  Score : " in a string array.
                    names.Add(r_textbox2[0]);                                                              //First variable of the array is the list of strings, names.                     
                    scores.Add(Convert.ToInt32(r_textbox2[1]));                                            //Second variable of the array is the list of ints(which needs to be converted first), scores.                        
                }

                for (int i = 0; i < scores.Count; i++)                                                     //A for-loop.                                                   
                {
                    if (i == 0)                                                                            //If i = 0 (scores list is empty).                                
                    {
                        top_scorer1 = scores[i];                                                           //Then, the current score is the highscore on easy difficulty.                             
                    }
                    if (scores[i] > top_scorer1)                                                           //If a score from the scores list is bigger than the temporary highscore.                  
                    {       
                        top_scorer1 = scores[i];                                                           //Then, is been set as new temporary highscore on easy difficulty.         
                    }
                }

                int index = scores.FindIndex(x => x == top_scorer1);                                       //An int-type variable index searches for the top_scorer1 element.                                                                         
                label5.Text = names[index];                                                                //Shows the name of the top scorer on easy difficulty, which exists on the list of strings, names.                        
                label6.Text = "Score : " + top_scorer1.ToString();                                         //Converts int-type variable top_score1 to string and shows the highscore on easy difficulty.                                                 

            }
            FileInfo f2 = new FileInfo("top_scorers2.txt");                                                //A fileInfo-type variable, f2 (top_scorers2.txt). 
            if (f2.Length != 0)                                                                            //If file f2 is not empty.                                         
            {
                List<string> names = new List<string>();                                                   //A list of strings, name.                                                     
                List<int> scores = new List<int>();                                                        //A list of ints, scores.                                                
                richTextBox2.LoadFile("top_scorers2.txt", RichTextBoxStreamType.PlainText);                //richTextBox2 loads f2("top_scorers2.txt").

                foreach (string s in richTextBox2.Lines)                                                   //Foreach string in richTextBox2.        
                {
                    string[] r_textbox2 = s.Split(new string[] { "  Score : " }, StringSplitOptions.None); //Variables Splited by "  Score : " in a string array.
                    names.Add(r_textbox2[0]);                                                              //First variable of the array is the list of strings, names.                   
                    scores.Add(Convert.ToInt32(r_textbox2[1]));                                            //Second variable of the array is the list of ints (which needs to be converted first), scores.                             
                }

                for (int i = 0; i < scores.Count; i++)                                                     //A for-loop.                                                                                 
                {
                    if (i == 0)                                                                            //If i = 0 (scores list is empty).                         
                    {
                        top_scorer2 = scores[i];                                                           //Then, the current score is the highscore on professional difficulty.                             
                    }
                    if (scores[i] > top_scorer2)                                                           //If a score from the scores list is bigger than the temporary highscore.                              
                    {
                        top_scorer2 = scores[i];                                                           //Then, is been set as new temporary highscore on professional difficulty.                         
                    }
                }

                int index = scores.FindIndex(x => x == top_scorer2);                                       //An int-type variable index searches for the top_scorer2 element.                                         
                label7.Text = names[index];                                                                //Shows the name of the top scorer on professional difficulty which exists on the list of strings, names.                               
                label8.Text = "Score : " + top_scorer2.ToString();                                         //Converts int-type variable top_score2 to string and shows the highscore on professional difficulty.                                                                         
            }

            scorekiller = true;                                                                            //Score does no count while form is loaded, but the Start is not clicked (Intro Screen).            
            music_player_Intro.controls.play();                                                            //The Intro song ("Intro.mp3") is playing while Start is not clicked (Intro Screen).         
            pictureBox1.ImageLocation = "Super_Mario.gif";                                                 //pictureBox1 loads the "Super_Mario.gif".
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);                                           //DoubleBuffer is set on true.        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!scorekiller)                                                                                             //If the game is not paused or is in the Intro Screen.                         
            {
                string[] hit_sounds = new string[3] { "sound_effect1.mp3", "sound_effect2.mp3", "sound_effect3.mp3" };    //A sting-type array of 3 songs("sound_effect1.mp3", "sound_effect2.mp3", "sound_effect3.mp3").

                int i = r.Next(0, 3);                                                                                     //Int i is a random number choice between 1,2,3.                          
                string sound_effect = hit_sounds[i];                                                                      //String sound_effect is a random choice from the string-type array hit_sounds.                    
                soundeffects.controls.play();                                                                             //The sound_effects are playing at every click of the pictureBox1.                       
                soundeffects.URL = sound_effect;                                                                          //the random choice (string sound_effect) is loaded by the player (soundeffects).                       
                label2.Text = "Score : " + score.ToString();                                                              //Converts int-type variable score to string and shows the score.                      
                if (easy)                                                                                                 //if difficulty level is been set on easy difficulty.                                          
                {   
                    score += 5;                                                                                           //At every click int-type variable score increases by 5.                                               
                }
                else                                                                                                      //if difficulty level is been set on professional difficulty.                                      
                {
                    score += 10;                                                                                          //At every click int-type variable score increases by 10.                                              
                }
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!easy && !professional)                                                                                   //If both "easy" and "professional" is false (i.e. No difficulty level is selected).                                                                                  
            {
                MessageBox.Show("Please select the level of difficulty you want.", "ERROR");                              //A MessageBox pops up.                                                                                                           
            }
            if (richTextBox1.Text == String.Empty)                                                                        //If richTextBox1 is empty (i.e. No name from the user).                                                                                                                                                  
            {
                MessageBox.Show("You can't play without a name.", "ERROR");                                               //A MessageBox pops up.                                                                               
            }
            else if (richTextBox1.Text != String.Empty && (easy || professional))                                         //If richTextBox1 is not empty and player has select a difficulty level (easy or professional).                                                                               
            {
                if (easy)                                                                                                 //If difficulty level is set on easy.                                                                                                              
                {
                    DialogResult dialogResult = MessageBox.Show("Try to catch Mario as many times as you can in order to get points.\nDifficulty has been set to Easy.\nYou have 60 seconds.", "Get ready?", MessageBoxButtons.OK);
                    //A MessageBox pops up with 1 possible answer (OK).
                    if (dialogResult == DialogResult.OK)                                                                  //If answer is "OK".
                    {
                        start = true;                                                                                     //Bool-type variable start is true.                  
                        scorekiller = false;                                                                              //Bool-type variable scorekiller is false (i.e. score counts).                                 
                        timer1.Enabled = true;                                                                            //Timer1 enabled.                  
                        timer2.Enabled = true;                                                                            //Timer2 enabled.                  
                        timer2.Interval = 1000;                                                                           //At every 1000ms (= 1 second) Timer2 Ticks.                                               
                        music_player_Intro.controls.stop();                                                               //The Intro song ("Intro.mp3") stops while Start is clicked (Play Screen).                                             
                        music_player1.URL = "Super Mario Bros.mp3";                                                       //The Play song ("Super Mario Bros.mp3") loads at music_player1.                                                  
                        music_player1.controls.play();                                                                    //The Play song ("Super Mario Bros.mp3") is playing when Start is clicked (Play Screen).                                                                   
                        pictureBox4.Visible = false;                                                                      //pictureBox4 is not visible.                                          
                        pictureBox5.Visible = false;                                                                      //pictureBox5 is not visible.                                  
                        pictureBox6.Visible = false;                                                                      //pictureBox6 is not visible.                                              
                        pictureBox7.Visible = false;                                                                      //pictureBox7 is not visible.                          
                        richTextBox2.Visible = false;                                                                     //richTextBox2 is not visible.  
                        startToolStripMenuItem.Enabled = false;                                                           //Start menu-button is not enabled.                                                                                                                          
                        aboutToolStripMenuItem.Enabled = false;                                                           //About menu-button is not enabled.                                                                                               
                        historyToolStripMenuItem.Enabled = false;                                                         //History menu-button is not enabled.                                                                                   
                        label1.Text = "Time : " + counter.ToString() + " seconds";                                        //Converts int-type variable counter to string and shows the remaining time.                                                                   
                    }
                }

                else if (professional)                                                                                    //If difficulty level is set on easy.
                {
                    DialogResult dialogResult = MessageBox.Show("Try to catch Mario as many times as you can in order to get points.\nBe careful because Mario is quicker now.\nDifficulty has been set to Professional.\nYou have 60 seconds.", "Are you ready?", MessageBoxButtons.OK);
                    //A MessageBox pops up with 1 possible answer (OK).
                    if (dialogResult == DialogResult.OK)                                                                  //If answer is "OK".                                                               
                    {
                        start = true;                                                                                     //Bool-type variable start is true.                                                                                                               
                        scorekiller = false;                                                                              //Bool-type variable scorekiller is false (i.e. score counts).                                                                 
                        timer1.Enabled = true;                                                                            //Timer1 enabled.                                                                            
                        timer2.Enabled = true;                                                                            //Timer2 enabled.                                                                                
                        timer2.Interval = 1000;                                                                           //At every 1000ms (= 1 second) Timer2 Ticks.                                                                            
                        music_player_Intro.controls.stop();                                                               //The Intro song ("Intro.mp3") stops while Start is clicked (Play Screen).                                                                                  
                        music_player1.URL = "Super Mario Bros.mp3";                                                       //The Play song ("Super Mario Bros.mp3") loads at music_player1.                                                        
                        music_player1.controls.play();                                                                    //The Play song ("Super Mario Bros.mp3") is playing when Start is clicked (Play Screen).                                                                                                                                    
                        pictureBox4.Visible = false;                                                                      //pictureBox4 is not visible.                                                                      
                        pictureBox5.Visible = false;                                                                      //pictureBox5 is not visible.                                                                                                                      
                        pictureBox6.Visible = false;                                                                      //pictureBox6 is not visible.                                                                          
                        pictureBox7.Visible = false;                                                                      //pictureBox7 is not visible.                             
                        richTextBox2.Visible = false;                                                                     //richTextBox2 is not visible.                                                                                                  
                        startToolStripMenuItem.Enabled = false;                                                           //Start menu-button is not enabled.                                                       
                        aboutToolStripMenuItem.Enabled = false;                                                           //About menu-button is not enabled.                                                           
                        historyToolStripMenuItem.Enabled = false;                                                         //History menu-button is not enabled.                               
                        label1.Text = "Time : " + counter.ToString() + " seconds";                                        //Converts int-type variable counter to string and shows the remaining time.                                                                                                                      
                    }
                }
            }
        }

        private void easyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;                                                                                  //pictureBox4 is not visible.    
            richTextBox2.Visible = true;                                                                                  //richTextBox2 is visible (shows past players).                                                                                                          
            richTextBox2.LoadFile("top_scorers1.txt", RichTextBoxStreamType.PlainText);                                   //richTextBox2 is loaded from "top_scorers1.txt" and shows past players.                                                                                                                                                                                                                    
        }

        private void professionalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;                                                                                  //pictureBox4 is not visible.    
            richTextBox2.Visible = true;                                                                                  //richTextBox2 is visible (shows past players).                                          
            richTextBox2.LoadFile("top_scorers2.txt", RichTextBoxStreamType.PlainText);                                   //richTextBox2 is loaded from "top_scorers2.txt" and shows past players.                                  
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {         
            DialogResult dialogResult = MessageBox.Show("Difficulty has been set to Easy.\nAre you ready to join Mario's world?","Press Start to play", MessageBoxButtons.YesNo);
            //A MessageBox pops up with 2 possible answers(Yes/No).
            if (dialogResult == DialogResult.Yes)                                                                         //If answer is "Yes".                                 
            {
                easy = true;                                                                                              //Difficulty level is set on easy.          
                professional = false;                                                                                     //Bool-type variable professional is false (i.e.game on easy).                                                   
                difficultyToolStripMenuItem.Enabled = false;                                                              //Difficulty menu-button is not enabled (i.e. game's difficulty cannot change while game is on).                                                                                                                                                                                                                         
                pictureBox1.Size = new Size(300, 270);                                                                    //pictureBox1 (Mario's) size changes.                                          
            }
            else if (dialogResult == DialogResult.No)                                                                     //If answer is "No", then do nothing.                                                                                                                                             
            {

            }
        }

        private void professionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Difficulty is set on Professional.\nAre you ready to join Mario's world?", "Press Start to play", MessageBoxButtons.YesNo);
            //A MessageBox pops up with 2 possible answers(Yes/No).
            if (dialogResult == DialogResult.Yes)                                                                         //If answer is "Yes".                                               
            {                                                                                                               
                easy = false;                                                                                             //Bool-type variable easy is false (i.e.game on professional).                                                                                                                     
                professional = true;                                                                                      //Difficulty level is set on professional.                                                                                                                                                       
                difficultyToolStripMenuItem.Enabled = false;                                                              //Difficulty menu-button is not enabled (i.e. game's difficulty cannot change while game is on).                     
                pictureBox1.Size = new Size(250, 250);                                                                    //pictureBox1 (Mario's) size changes.                                                   
            }   
            else if (dialogResult == DialogResult.No)                                                                     //If answer is "No", then do nothing.                                                                        
            {

            }  
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();                                                                                           // Exit from the Application.  
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This game was made by Dimitrios Matsanganis.");                                              //A Messagebox pops up with editor's name.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            int W1 = this.Width;                                                                                          //Get the Width of the Form.
            int H1 = this.Height;                                                                                         //Get the Height of the Form. 

            if (easy)                                                                                                     //If Difficult level is set on Easy.
            {
                timer1.Interval = 400;                                                                                    //Every tick (Mario's movement), every 400ms.      
                int[] Random_Easy_Movement = new int[3] { -150, 0, 150 };                                                 //A sting-type array of 3 ints (-150, 0, 150).                     

                int value1 = r.Next(0, 3);                                                                                //Int-type variable, value1 is a random number choice.                                                                                                                                                 
                int value2 = r.Next(0, 3);                                                                                //Int-type variable, value2 is a random number choice.                                                                                                                            
                int X_Easy_Movement = Random_Easy_Movement[value1];                                                       //Int-type variable, X_Easy_Movement is a random choice from the "Random_Easy_Movement" array.                                                                                                     
                int Y_Easy_Movement = Random_Easy_Movement[value2];                                                       //Int-type variable, Y_Easy_Movement is a random choice from the "Random_Easy_Movement" array.                                                     
                int X = pictureBox1.Location.X + X_Easy_Movement;                                                         //Int-type variable, X is the move of the pictureBox1 on x-axis.                                       
                int Y = pictureBox1.Location.Y + Y_Easy_Movement;                                                         //Int-type variable, Y is the move of the pictureBox1 on y-axis.                                                       
                pictureBox1.Location = new Point(X, Y);                                                                   //New pictureBox1 (Mario) location.                                                                              

                if (X > (W1 - 300))                                                                                       //If X is bigger than the Width of the Form without Mario's Width (pictureBox1,300px).                                                         
                {                                                                                                                                                                                                                   
                    pictureBox1.Location = new Point(r.Next(300,(W1-300)), Y);                                            //New pictureBox1 (Mario) location.                                                                                           
                }                                                                       
                else if (X < 300)                                                                                         //If X is less than Mario's Width (pictureBox1,300px).                                                                                      
                {
                    pictureBox1.Location = new Point(300, Y);                                                             //New pictureBox1 (Mario) location.                                             
                }
                if (Y > (H1 - 270))                                                                                       //If Y is bigger than the Height of the Form without Mario's Height (pictureBox1,270px).                                                
                {
                    pictureBox1.Location = new Point(X, r.Next(250, (H1 - 270)));                                         //New pictureBox1 (Mario) location.                                                                                                           
                }
                else if (Y < 270)                                                                                         //If Y is less than Mario's Height (pictureBox1,270px).                                                                          
                {
                    pictureBox1.Location = new Point(X,270);                                                              //New pictureBox1 (Mario) location.                                                                               
                }

            }
            else if (professional)                                                                                        //If Difficult level is set on Professional.
            {
                timer1.Interval = 350;                                                                                    //Every tick (Mario's movement), every 350ms.                                                 
                int[] Random_Professional_Movement = new int[5] { -250, -150, 0, 150, 250 };                              //A sting-type array of 5 ints (-250, -150, 0, 150, 250).                                          

                int value1 = r.Next(0, 5);                                                                                //Int-type variable, value1 is a random number choice.                                          
                int value2 = r.Next(0, 5);                                                                                //Int-type variable, value1 is a random number choice.                                                              
                int X_Professional_Movement = Random_Professional_Movement[value1];                                       //Int-type variable, X_Professional_Movement is a random choice from the "Random_Professional_Movement" array.                                                                  
                int Y_Professional_Movement = Random_Professional_Movement[value2];                                       //Int-type variable, X_Professional_Movement is a random choice from the "Random_Professional_Movement" array.                                                                                  
                int X = pictureBox1.Location.X + X_Professional_Movement;                                                 //Int-type variable, X is the move of the pictureBox1 on x-axis.                                                  
                int Y = pictureBox1.Location.Y + Y_Professional_Movement;                                                 //Int-type variable, Y is the move of the pictureBox1 on y-axis.                                                                   
                pictureBox1.Location = new Point(X, Y);                                                                   //New pictureBox1 (Mario) location.                                                                                                                                 

                if (X > (W1 - 250))                                                                                       //If X is bigger than the Width of the Form without Mario's Width (pictureBox1,250px).                                             
                {                                                                                                                                                                                                       
                    pictureBox1.Location = new Point(r.Next(250,(W1 - 250)), Y);                                          //New pictureBox1 (Mario) location.                                                                                                                                           
                }                                                                                                               
                else if (X < 250)                                                                                         //If X is less than Mario's Width (pictureBox1,250px).                                                                                                                 
                {
                    pictureBox1.Location = new Point(250, Y);                                                             //New pictureBox1 (Mario) location.                                                                                          
                }
                if (Y > (H1 - 250))                                                                                       //If Y is bigger than the Height of the Form without Mario's Height (pictureBox1,250px).                                                                     
                {
                    pictureBox1.Location = new Point(X, r.Next(250, (H1 - 250)));                                         //New pictureBox1 (Mario) location.                                                                                                                                                                                                                   
                }
                else if (Y < 250)                                                                                         //If Y is less than Mario's Height (pictureBox1,250px).                        
                {
                    pictureBox1.Location = new Point(X,250);                                                              //New pictureBox1 (Mario) location.                   
                }

            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            counter--;                                                                                        //At every Timer2 Tick(1000ms), int-type variable counter is subtracted by 1. 
            label1.Text = "Time : " + counter.ToString() + " seconds";                                        //Converts int-type variable counter to string and shows the time remaining (label1).            
             
            if (counter == 0)                                                                                 //If int-type variable counter = 0 (Game is Over).    
            {
                timer1.Stop();                                                                                //Stops Mario's movement (pictureBox1,Timer1).                                           
                timer2.Stop();                                                                                //Stops clock's countdown (label1,Timer2).                       

                if (easy)                                                                                     //if difficulty level is been set on easy difficulty.  
                {
                    FileInfo f1 = new FileInfo("top_scorers1.txt");                                           //A fileInfo-type variable, f1 (top_scorers1.txt).                          
                    StreamWriter sw = new StreamWriter("top_scorers1.txt", true);                             //A streamWriter-type variable, sw writes to (top_scorers1.txt).         

                    if (f1.Length == 0)                                                                       //If f1 (top_scorers1.txt) is empty.     
                    {
                        sw.Write("Name : " + richTextBox1.Text + "  Score : " + score);                       //Then, sw writes.             
                    }
                    else if (f1.Length != 0)                                                                  //If f1 (top_scorers1.txt) is not empty.
                    {
                        sw.Write(Environment.NewLine + "Name : " + richTextBox1.Text + "  Score : " + score); //Then, sw writes in a new line. 
                    }
                    sw.Close();                                                                               //StreamWriter-type variable, sw closes.         
                    if (score > top_scorer1)                                                                  //If score is bigger than the current highscore (top_scorer1).  
                    {
                        MessageBox.Show("NEW HIGHSCORE!");                                                    //A MessageBox pops up.
                        label5.Text = "Name : " + richTextBox1.Text;                                          //label5's text changes and shows the name (richTextBox1.Text) of the new top scorer.                                                       
                        label6.Text = "  Score : " + score;                                                   //label6's text changes and shows the score (score) of the new top scorer.                             
                    }
                    else if (score < top_scorer1)                                                             //If score is less than the current highscore (top_scorer1), then do nothing.               
                    {

                    }
                }
                else if (professional)                                                                        //if difficulty level is been set on professional difficulty.   
                {
                    FileInfo f2 = new FileInfo("top_scorers2.txt");                                           //A fileInfo-type variable, f2 (top_scorers2.txt).                  
                    StreamWriter sw = new StreamWriter("top_scorers2.txt", true);                             //A streamWriter-type variable, sw writes to (top_scorers2.txt).                            

                    if (f2.Length == 0)                                                                       //If f2 (top_scorers2.txt) is empty.          
                    {
                        sw.Write("Name : " + richTextBox1.Text + "  Score : " + score);                       //Then, sw writes.   
                    }
                    else if (f2.Length != 0)                                                                  //If f2 (top_scorers2.txt) is not empty.  
                    {
                        sw.Write(Environment.NewLine + "Name : " + richTextBox1.Text + "  Score : " + score); //Then, sw writes in a new line.  

                    }
                    sw.Close();                                                                               //StreamWriter-type variable, sw closes.               
                    if (score > top_scorer2)                                                                  //If score is bigger than the current highscore (top_scorer2).    
                    {
                        MessageBox.Show("NEW HIGHSCORE!");                                                    //A MessageBox pops up.                                             
                        label7.Text = "Name : " + richTextBox1.Text;                                          //label7's text changes and shows the name (richTextBox1.Text) of the new top scorer.                                          
                        label8.Text = "  Score : " + score;                                                   //label8's text changes and shows the score (score) of the new top scorer.                                              
                    }
                    else if (score < top_scorer2)                                                             //If score is less than the current highscore (top_scorer2), then do nothing.                                                                                                     
                    {   

                    }
                }

                DialogResult dialogResult = MessageBox.Show("Do you want to play again?", "THE GAME IS OVER!", MessageBoxButtons.YesNo);
                //A MessageBox pops up with 2 possible answers(Yes/No).

                if (dialogResult == DialogResult.Yes)                                                         //If answer is "Yes" (i.e.Resets the Intro Screen). 
                {
                    score = 0;                                                                                //Int-type variable, score resets to zero for a possible new game round.                                                                                                                         
                    counter = 60;                                                                             //Int-type variable, counter resets to 60 for a possible new game round.                      
                    start = false;                                                                            //Bool-type variable start is false, so the Start button has to be clicked for a new round.                  
                    easy = false;                                                                             //Bool-type variable professional is false, so user has to select the difficulty level he/she wants in order to play again.                              
                    professional = false;                                                                     //Bool-type variable easy is false, so user has to select the difficulty level he/she wants in order to play again.                                          
                    scorekiller = true;                                                                       //Bool-type variable scorekiller is true, so the score does not count.                                      
                    label1.Text = "Time : " + counter.ToString() + " seconds";                                //Converts int-type variable counter to string and shows the time.                                                                         
                    label2.Text = "Score : " + score.ToString();                                              //Converts int-type variable score to string and shows the score.                                                                      
                    music_player1.controls.stop();                                                            //The Play song ("Super Mario Bros.mp3") stops when Start is not clicked (Intro Screen).                                      
                    music_player_Intro.controls.play();                                                       //The Intro song ("Intro.mp3") is playing while Start is not clicked (Intro Screen).                
                    pictureBox1.Size = new Size(206, 270);                                                    //pictureBox1 (Mario) resets its Intro Screen size.                                                                                                                                                                                                                     
                    pictureBox1.Location = new Point(325, 235);                                               //pictureBox1 (Mario) resets its Intro Screen location.                                     
                    pictureBox4.Visible = true;                                                               //pictureBox4 is visible again.               
                    pictureBox5.Visible = true;                                                               //pictureBox5 is visible again.                                                                    
                    pictureBox6.Visible = true;                                                               //pictureBox6 is visible again.      
                    pictureBox7.Visible = true;                                                               //pictureBox7 is visible again.                  
                    richTextBox2.Visible = false;                                                             //richTextBox2 is not visible.              
                    startToolStripMenuItem.Enabled = true;                                                    //Start menu-button is enabled again.               
                    aboutToolStripMenuItem.Enabled = true;                                                    //About menu-button is enabled again.                              
                    historyToolStripMenuItem.Enabled = true;                                                  //History menu-button is enabled again.                      
                    difficultyToolStripMenuItem.Enabled = true;                                               //Difficulty menu-button is enabled again, so that the user can select the difficulty level he/she wants in order to play again.                                                       

                }
                else if (dialogResult == DialogResult.No)                                                     //If answer is "No".         
                {
                    Application.Exit();                                                                       //Exit from the Application.                                
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (start)                                                                                        //If Start button is clicked so the game is on.                                                                                                  
            {
                if (!scorekiller)                                                                             //If scorekiller = false, which means that the game is playing.                                                             
                {
                    scorekiller = true;                                                                       //On click the game is paused and the score is not counted (scorekiller = true).
                    timer1.Stop();                                                                            //Stops Mario's movement (pictureBox1,Timer1).                      
                    timer2.Stop();                                                                            //Stops clock's countdown (label1,Timer2).                                                                                                                                                                                                              
                    button1.BackgroundImage = WindowsFormsApp9.Properties.Resources.play_button;              //Changes the button1 background image to play_button.                                                                                                                                  
                }
                else if (scorekiller)                                                                         //If scorekiller = true, which means that the game is paused.              
                {
                    scorekiller = false;                                                                      //On click the game is not paused and the score is counted (scorekiller = false). 
                    timer1.Start();                                                                           //Resumes Mario's movement (pictureBox1,Timer1).                                                                                                          
                    timer2.Start();                                                                           //Resumes clock's countdown (label1,Timer2).                                                                                                                                                                   
                    button1.BackgroundImage = WindowsFormsApp9.Properties.Resources.pause_button;             //Changes the button1 background image to pause_button.                                                                                                                          
                }
            }        
            else if (!start)                                                                                  //If Start is not clicked,then do nothing.         
            {

            }
        }
    }
}


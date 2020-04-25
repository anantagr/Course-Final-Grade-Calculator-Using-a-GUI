// Program Name:    Course Final Grade Calculator Using a GUI

// Author:          Anant Agarwal

// Date:            06 April 20

//Description:      Assignment 2
//                      The intent of this application is to provide a tool to instructors that they can use to calculate
//                      the final mark of a course as both a percentage and letter grade. When run, the application will
//                      display a window form with data entry fields for quiz marks, a midterm mark, and a final examination
//                      mark. The final grade is calculated using the weightings of 20% for the average of the quizzes, 30% 
//                      for the midterm mark, and 50% for the final grade. The final grade percentage mark is converted to a 
//                      letter grade using the same table included in the later section.

//Information provided by the user:
//                      Percentage Grade to Letter Grade Conversion Table:
//                          Letter Grade                Percentage
//                              A+                       95 - 100
//                              A                        90 - 94
//                              A-                       85 - 89
//                              B+                       80 - 84
//                              B                        75 - 79
//                              B-                       70 - 74
//                              C+                       67 - 69
//                              C                        64 - 66
//                              C-                       60 - 63
//                              D+                       55 - 59
//                              D                        50 - 54
//                              F                        0  - 49

//Using the Application:    This application will require following inputs.
//                          1. Quiz marks
//                          2. Midterm Exam mark
//                          3. Final Exam mark

//                          *** Midterm and Final exam mark and at least one Quiz mark are mandatory, without which the
//                          application will not execute.
//                          <Enter> key can be used to input quiz marks which will be display on the right - side table.
//                          <Tab> key can be used to navigate between buttons and entry fields within the application.
//                          Keyboard shortcuts are as under :
//                              Alt + M to delete list of quiz marks.
//                              Alt + D to turn ON/OFF the Highest & Lowest quiz mark drop option.
//                              Alt + C to calculate final mark and grade.
//                              Alt + R to delete all the data entered in the application.
//                              Alt + Q to quit the application

//                          This application provide an option to omit highest and lowest quiz mark and re-calculate the
//                          final grade. That option is available only if 5 or more Quiz marks have been entered.

//                          Data limitation :
//                              -   Marks should be between 0 to 100
//                              -   Marks should be whole number, decimal entries not accepted

//                          Application limitations :
//                              -   Final mark calculated is rounded to the nearest whole number.
//                              -   Marks entered cannot be recovered if Reset button is clicked.

//                          There were no compilation errors in the program



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2___A_Course_Final_Grade_Calculator_Using_a_GUI
{
    public partial class Form1 : Form
    {
        //We have defined a "getValue" function to validated the data entered by the user using if-else ladder.
        //This function will run following validation :
        //                  --  Input value is not empty.
        //                  --  Input value is not non-integral.
        //                  --  Input value is whole number.
        //                  --  Input value is between 0 and 100
        // If the data is not validated, then the function will prompt an error message based on error type.
        // If the data is good value, fucntion will return it as "value".
        public bool getValue(string num, out double value)
        {
            if (!double.TryParse(num, out value))
            {
                if (num.Length == 0)
                {
                    MessageBox.Show("***Error - No Quiz Marks entered, Please enter a value");
                    return false;
                }
                else
                {
                    MessageBox.Show("***Error - Non-integral value entered, Please re-enter");
                    return false;
                } 
            }
            
            if(value < 0 || value > 100)
            {
                MessageBox.Show("***Error - Marks will be between 0 and 100, please re-enter");
                return false;
            }

            if(value % 1 != 0)
            {
                MessageBox.Show("***Error - Mark entered cannot have decimals. Please re-enter");
                return false;
            }

            return true;
        }

        //declating the variables
        double quizMark;            // variavle to hold the individual quiz mark
        double quizTotal = 0;       // variavle to hold total of all quiz marks
        double quizTotalNew;        // variavle to hold second copy total of all quiz marks
        int numberOfQuizzes = 0;    // variavle to hold number of quiz marks entered
        int numberOfQuizzesNew;     // variavle to hold second copy of number of quiz marks entered
        double maxQuizMark = 0;     // variavle to hold maximum quiz marks
        double minQuizMark = 0;     // variavle to hold minimum quiz mark
        double midTermMark;         // variavle to hold MidTerm mark
        double finalExamMark;       // variavle to hold Final Exam mark
        double averagequizMarks;    // variavle to hold average of all quiz marks
        double finalGradeMark;      // variavle to hold Final mark obtained
        string finalGrade;          // variavle to hold Final grade obtained

        public Form1()
        {
            InitializeComponent();
        }

        //Clicking the Quit button will exit the application.
        private void quitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //By clicking TransferMark button, we are send the user input to "getValue" function for cleansing,
        //If the value is good then it is stored as quizMark in list of marks in Multiline text box
        //We have also intialized a minimum and maximum mark variable which will compare new inputs and store max and min
        //values for later process.
        //We are also calculation the number of Quiz marks entered for calculating quiz average.
        private void transferMarksButton_Click(object sender, EventArgs e)
        {
            
            if (!getValue(EnterQuizMarkTextBox.Text, out quizMark))
            {
                EnterQuizMarkTextBox.Select();
                return;
            }
            numberOfQuizzes += 1;
            if (numberOfQuizzes == 1)
            {
                maxQuizMark = quizMark;
                minQuizMark = quizMark;

            }
            else
            {
                if (quizMark > maxQuizMark)
                {
                    maxQuizMark = quizMark;
                }

                if (quizMark < minQuizMark)
                {
                    minQuizMark = quizMark;
                }
            }
            listOfQuizMarksTextBox.AppendText(EnterQuizMarkTextBox.Text + Environment.NewLine);
            quizTotal = quizTotal + quizMark;
            EnterQuizMarkTextBox.Select();
            EnterQuizMarkTextBox.Text = String.Empty;

            quizTotalNew = quizTotal;
            numberOfQuizzesNew = numberOfQuizzes;

            if (numberOfQuizzes > 4)
            {
                highLowQuizCheckbox.Visible = true;
            }
            return;
        }

        private void EnterQuizMarkTextBox_Leave(object sender, EventArgs e)
        {

        }
       
        //Using this checkbox, we are checking if user wants to omit Highest and lowest quiz marks to 
        //recalculate the grade.
        private void highLowQuizCheckbox_Click(object sender, EventArgs e)
        {
            quizTotalNew = quizTotal;
            numberOfQuizzesNew = numberOfQuizzes;

            if (highLowQuizCheckbox.Checked == true)
            {
                quizTotalNew = quizTotal - maxQuizMark - minQuizMark;
                numberOfQuizzesNew = numberOfQuizzes - 2;
            }
            return;
        }

        //This function get activated when we click "Calculate Grade" button, it send the Midterm and Final Exam marks
        //to "getValue" function for cleansing. When a good values is recieved, it is used to calculate Final mark and 
        //final grade. The weightage of marks is as follows: 20% from Quiz average mark, 30% from Midterm mark and 50%
        //from Final Exam mark.
        //Final grade is decided using if-else ladder based on table provided by the user.
        private void calculateGradeButton_Click(object sender, EventArgs e)
        {
            //This is to ensure, user input atleast one quiz mark to calculate final grades
            if(numberOfQuizzes<1)
            {
                MessageBox.Show($"***Error - Minimum one Quiz Mark required");
                EnterQuizMarkTextBox.Select();
                return;
            }

            //Sending midterm mark for validation
            if (!getValue(midtermMarksTextBox.Text, out midTermMark))
            {
                midtermMarksTextBox.Select();
                return;
            }

            //Sending final exam mark for validation
            if (!getValue(finalExamMarksTextBox.Text, out finalExamMark))
            {
                finalExamMarksTextBox.Select();
                return;
            }
            
            //Calculating average quiz mark and final mark and grade obtained
            averagequizMarks = quizTotalNew/ numberOfQuizzesNew;
            finalGradeMark = (Math.Round((averagequizMarks * 0.2) + (midTermMark * 0.3) + (finalExamMark * 0.5)));
            percentageTextBox.Text = finalGradeMark.ToString();

            if (finalGradeMark >= 95)
            {
                finalGrade = "A+";
            }
            else if (finalGradeMark >= 90)
            {
                finalGrade = "A";
            }
            else if (finalGradeMark >= 85)
            {
                finalGrade = "A-";
            }
            else if (finalGradeMark >= 80)
            {
                finalGrade = "B+";
            }
            else if (finalGradeMark >= 75)
            {
                finalGrade = "B";
            }
            else if (finalGradeMark >= 70)
            {
                finalGrade = "B-";
            }
            else if (finalGradeMark >= 67)
            {
                finalGrade = "C+";
            }
            else if (finalGradeMark >= 64)
            {
                finalGrade = "C";
            }
            else if (finalGradeMark >= 60)
            {
                finalGrade = "C-";
            }
            else if (finalGradeMark >= 55)
            {
                finalGrade = "D+";
            }
            else if (finalGradeMark >= 50)
            {
                finalGrade = "D";
            }
            else
            {
                finalGrade = "F";
            }

            gradeTextBox.Text = finalGrade;
            return;
        }

        private void EnterQuizMarkTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        //This function run when user clicks Reset Quiz marks button
        //It deletes all the quiz marks entered and reset counters for recalculation
        private void resetQuizMarksButton_Click(object sender, EventArgs e)
        {
            EnterQuizMarkTextBox.Text = String.Empty;
            listOfQuizMarksTextBox.Text = String.Empty;
            numberOfQuizzes = 0;
            quizTotal = 0;
            maxQuizMark = 0;
            minQuizMark = 0;
            quizMark = 0;

            percentageTextBox.Text = String.Empty;
            gradeTextBox.Text = String.Empty;

            highLowQuizCheckbox.Visible = false;

            EnterQuizMarkTextBox.Select();

        }

        //This function run when user clicks Reset All marks button to use it for new user
        //It deletes all the quiz marks, midterm and final marks entered and reset counters for recalculation
        private void resetAllMarksButton_Click(object sender, EventArgs e)
        {
            EnterQuizMarkTextBox.Text = String.Empty;
            listOfQuizMarksTextBox.Text = String.Empty;
            midtermMarksTextBox.Text = String.Empty;
            finalExamMarksTextBox.Text = String.Empty;
            percentageTextBox.Text = String.Empty;
            gradeTextBox.Text = String.Empty;
           
            numberOfQuizzes = 0;
            numberOfQuizzesNew = 0;
            quizTotal = 0;
            quizTotalNew = 0;
            maxQuizMark = 0;
            minQuizMark = 0;
            quizMark = 0;
            finalGradeMark = 0;

            highLowQuizCheckbox.Visible = false;

            EnterQuizMarkTextBox.Select();
        }

        //This function deletes any calculated final grade or final marks if midterm / final mark entered is changed
        private void midtermMarksTextBox_TextChanged(object sender, EventArgs e)
        {
            percentageTextBox.Text = String.Empty;
            gradeTextBox.Text = String.Empty;
        }
       
        private void finalExamMarksTextBox_TextChanged(object sender, EventArgs e)
        {
            percentageTextBox.Text = String.Empty;
            gradeTextBox.Text = String.Empty;
        }

        //This provide Alt + keys functionality to buttons and checkboxes in the application
        private void resetQuizMarksButton_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Alt == true && e.KeyCode == Keys.M)
            {
                resetQuizMarksButton.PerformClick();
            }
        }

        private void calculateGradeButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == true && e.KeyCode == Keys.C)
            {
                calculateGradeButton.PerformClick();
            }
        }

        private void resetAllMarksButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == true && e.KeyCode == Keys.R)
            {
                resetAllMarksButton.PerformClick();
            }
        }

        private void quitButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == true && e.KeyCode == Keys.Q)
            {
                quitButton.PerformClick();
            }
        }

        private void EnterQuizMarkTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                transferMarksButton.PerformClick();
            }
        }

        private void highLowQuizCheckbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == true && e.KeyCode == Keys.D)
            {
                highLowQuizCheckbox.Checked = true;
            }
        }

        //This function was used to give color effect when hovering mouse over button but later dropped so no values here
        private void transferMarksButton_MouseHover(object sender, EventArgs e)
        {
        }

        private void transferMarksButton_MouseLeave(object sender, EventArgs e)
        {
        }

        private void resetQuizMarksButton_MouseHover(object sender, EventArgs e)
        {
        }

        private void resetQuizMarksButton_MouseLeave(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}

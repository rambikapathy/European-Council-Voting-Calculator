using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// This initalises all of the required modules for the program to run.

namespace EU_Voting_Calculator
{
    public partial class Form1 : Form // This partial class for this 'Form' is used to store data into multiple files, but when compiling, brings them all back together as one.
    {
        static Country austria = new Country("Austria", 1.98f);
        static Country belgium = new Country("Belgium", 2.56f);
        static Country bulgaria = new Country("Bulgaria", 1.56f);
        static Country croatia = new Country("Croatia", 0.91f);
        static Country cyprus = new Country("Cyprus", 0.2f);
        static Country czechia = new Country("Czech Republic", 2.35f);
        static Country denmark = new Country("Denmark", 1.3f);
        static Country estonia = new Country("Estonia", 0.3f);
        static Country finland = new Country("Finland", 1.23f);
        static Country france = new Country("France", 14.98f);
        static Country germany = new Country("Germany", 18.54f);
        static Country greece = new Country("Greece", 2.4f);
        static Country hungary = new Country("Hungary", 2.18f);
        static Country ireland = new Country("Ireland", 1.1f);
        static Country italy = new Country("Italy", 13.65f);
        static Country latvia = new Country("Latvia", 0.43f);
        static Country lithuania = new Country("Lithuania", 0.62f);
        static Country luxembourg = new Country("Luxembourg", 0.14f);
        static Country malta = new Country("Malta", 0.11f);
        static Country netherlands = new Country("Netherlands", 3.89f);
        static Country poland = new Country("Poland", 8.49f);
        static Country portugal = new Country("Portugal", 2.3f);
        static Country romania = new Country("Romania", 4.34f);
        static Country slovakia = new Country("Slovakia", 1.22f);
        static Country slovenia = new Country("Slovenia", 0.47f);
        static Country spain = new Country("Spain", 10.49f);
        static Country sweden = new Country("Sweden", 2.29f);
        // The above chunk of code defines the static objects of each country; the name of the country, and population percentage.


        static List<Country> allCountriesparticipating = new List<Country> { austria, belgium, bulgaria, croatia, cyprus,
        czechia,denmark, estonia, finland, france, germany, greece, hungary, ireland, italy, latvia, lithuania,
        luxembourg, malta, netherlands, poland, portugal, romania, slovakia, slovenia, spain,sweden};
        // The above code creates a list of all of the countries that participate in the voting.
        // Like before, this is static so it does not change throughout the running of the program.
        // The static objects from before are used in this list.




        VotingRule qualifiedMajority = new VotingRule(15, 65, true);
        VotingRule reinforcedQualifiedmajority = new VotingRule(20, 65, true);
        VotingRule simpleMajority = new VotingRule(14, 0, false);
        VotingRule unanimity = new VotingRule(27, 0, false);
        // This code defines each of the types of voting that are available to choose from by the user,
        //as objects using the VotingRule class. Each voting type has different requirements for an "approved" status to be reached.
        // The first parameter in 'VotingRule' is the number of member states required to state "YES" for the vote to pass. 
        // The second parameter is the population percentage required.
        // The third parameter determines if a blocking minority is available for that particular voting system or not.

        public VotingRule currentRule;
        // This stores the voting system that the user selects as public object variable that uses the VotingRule class



        public Form1()
        {
            InitializeComponent();
        }




        public class Country
        {
            string name; // Each country is given a name as a string
            float populationPercentage; //  Each country is given a population percentage field as a floating point number

            string vote; 
            // The vote that the country chooses is stored as a string value
            //(this can be one of three values: "YES", "NO" or "ABSTAIN").

            public string voteProperty
            {
                get { return vote; }
                set { vote = value; }
            }
            // A property for the vote field. This is used to get and set the vote field outside of the class,
            //as all attributes are kept private.

            public float populationProperty
            {
                get { return populationPercentage; }
                set { populationPercentage = value; }
            }
            // A property for the populationPercentage field. Used to get and set the populationPercentage outside of the class,
            //as all attributes are kept private.

            public Country(string givenName, float givenPercentage)
            {
                name = givenName;
                populationPercentage = givenPercentage;
                vote = "YES";
            }
            // Each country is defined by a name and a population value. This is the constructor for the Country class
            // When the voting form initalises and first loads for the user, all country votes are automatically set to "YES".

        }
        // The class above is the 'Country' class.



        public class VotingRule
        {
            int membersRequired;
            int populationRequired;
            // The above attributes are used to compare population values that are collected with each vote change 
            //against these fixed values. These determine what requirements need to be met in order for votes to be approved.

            int amountYes;
            int amountNo;
            int amountAbstain;
            // These integer fields are used to count the votes of each option ("YES", "NO" and "ABSTAIN").

            float populationYes;
            float populationNo;
            float populationAbstain;
            // These float values are used to count each country's population values in order to calculate
            //if population values are met in order to pass votes.

            bool blockingMinority;
            // This is the boolean attribute for if the voting rule involves a blocking minority.

            public VotingRule(int givenStates, int givenpopulation, bool givenBool)
            {
                membersRequired = givenStates;
                populationRequired = givenpopulation;
                blockingMinority = givenBool;
            }
            // Constructor for the VotingRule class. The three attributes are given values by the object declarartions.


            public void GetResult(TextBox text)
            {
                amountYes = 0;
                amountNo = 0;
                amountAbstain = 0;
                populationYes = 0.0f;
                populationNo = 0.0f;
                populationAbstain = 0.0f;
                // All of the above values are currently set to '0', as they will be counted when votes are selected
                //or changed by the user.
                // This includes counting when the program starts, as all votes by default are set to "YES".


                for(int i = 0; i < allCountriesparticipating.Count; i++)
                {
                    if(allCountriesparticipating[i].voteProperty == "YES")
                    {
                        amountYes += 1;
                        populationYes += allCountriesparticipating[i].populationProperty;
                    }

                    if(allCountriesparticipating[i].voteProperty == "NO")
                    {
                        amountNo += 1;
                        populationNo += allCountriesparticipating[i].populationProperty;
                    }

                    if(allCountriesparticipating[i].voteProperty == "ABSTAIN")
                    {
                        amountAbstain += 1;
                        populationAbstain += allCountriesparticipating[i].populationProperty;
                    }
                }
                // For each country, the votes for each option of "YES", "NO" and "ABSTAIN" are counted,
                //as well as the population of those countries.


                if(blockingMinority == true)
                {
                    BlockingMinority(text);
                }
                // If the selected voting system has the option of a blocking minority, the BlockingMinority method is called.

                else if(membersRequired == 14)
                {
                    SimpleMajority(text);
                }
                // If the selected voting system requires 14 member states to say "YES",
                //the 'Simple Majority' method is called, as this can only occur if the rule is set to Simple Majority.

                else if((populationYes >= populationRequired) && (amountYes >= (membersRequired - amountAbstain)))
                {
                    text.Text = $"Result: Approved   Member States: Yes: {amountYes}   No: {amountNo}     " +
                        $"Abstain: {amountAbstain}           Population: Yes: {Math.Round(populationYes)}%    No: {Math.Round(populationNo)}%" +
                        $"          Abstain: {Math.Round(populationAbstain)}%";

                }
                // If the minimum population required value is met and the amount of member states required to say "YES" is met,
                //the vote is approved.

                else if((populationYes < populationRequired) && (amountYes < (membersRequired - amountAbstain)))
                {
                    text.Text = $"Result: Rejected   Member States: Yes: {amountYes}   No: {amountNo}     " +
                        $"Abstain: {amountAbstain}           Population: Yes: {Math.Round(populationYes)}%    No: {Math.Round(populationNo)}%" +
                        $"          Abstain: {Math.Round(populationAbstain)}% ";
                }
                // If the population required isn't met and neither is the number of member states, the vote is rejected.

                else 
                {
                    text.Text = $"Result: Rejected   Member States: Yes: {amountYes}   No: {amountNo}     " +
                        $"Abstain: {amountAbstain}           Population: Yes: {Math.Round(populationYes)}%    No: {Math.Round(populationNo)}%" +
                        $"          Abstain: {Math.Round(populationAbstain)}% ";
                }
                // If none of the above two conditions are met, the vote is automatically rejected.
            }

            private void BlockingMinority(TextBox text)
            {
                int noAndabstain = amountNo + amountAbstain;
                // The member states that voted "NO" and "ABSTAIN" were counted previously, and are now combined.

                if ((populationYes >= populationRequired) && (amountYes >= membersRequired))
                {
                    text.Text = $"Result: Approved   Member States: Yes: {amountYes}   No: {amountNo}     " +
                        $"Abstain: {amountAbstain}           Population: Yes: {Math.Round(populationYes)}%    No: {Math.Round(populationNo)}%" +
                        $"          Abstain: {Math.Round(populationAbstain)}%";
                }

                else if ((populationYes < populationRequired) && (noAndabstain < 4))
                {
                    text.Text = $"Result: Approved - Blocking Minority   Member States: Yes: {amountYes}   No: {amountNo}     " +
                        $"Abstain: {amountAbstain}           Population: Yes: {Math.Round(populationYes)}%    No: {Math.Round(populationNo)}%" +
                        $"          Abstain: {Math.Round(populationAbstain)}% ";
                }
                // If the number of member states that vote "NO" or "ABSTAIN" is less than 4, the vote is approved.
                // This is because the minimum number of states required to reject a vote is 4.

                else if ((populationNo > populationRequired) && (noAndabstain >= 4))
                {
                    text.Text = $"Result: Rejected   Member States: Yes: {amountYes}   No: {amountNo}     " +
                        $"Abstain: {amountAbstain}           Population: Yes: {Math.Round(populationYes)}%    No: {Math.Round(populationNo)}%" +
                        $"          Abstain: {Math.Round(populationAbstain)}% ";
                }
                // If the population required of member states voting "NO" or "ABSTAIN" is greater than the minimum required
                //for a blocking minority (above 35%), and if the number of states that vote for these options is greater than
                //or equal to 4, the vote is rejected.

                else
                {
                    text.Text = $"Result: Rejected   Member States: Yes: {amountYes}   No: {amountNo}     " +
                        $"Abstain: {amountAbstain}           Population: Yes: {Math.Round(populationYes)}%    No: {Math.Round(populationNo)}%" +
                        $"          Abstain: {Math.Round(populationAbstain)}% ";
                }
            }

            private void SimpleMajority(TextBox text)
            {
                if ((populationYes >= populationRequired) && (amountYes >= membersRequired))
                {
                    text.Text = $"Result: Approved   Member States: Yes: {amountYes}   No: {amountNo}     " +
                        $"Abstain: {amountAbstain}           Population: Yes: {Math.Round(populationYes)}%    No: {Math.Round(populationNo)}%" +
                        $"          Abstain: {Math.Round(populationAbstain)}%";

                }

                else if ((populationYes < populationRequired) && (amountYes < membersRequired))
                {
                    text.Text = $"Result: Rejected   Member States: Yes: {amountYes}   No: {amountNo}     " +
                        $"Abstain: {amountAbstain}           Population: Yes: {Math.Round(populationYes)}%    No: {Math.Round(populationNo)}%" +
                        $"          Abstain: {Math.Round(populationAbstain)}% ";
                }

                else
                {
                    text.Text = $"Result: Rejected   Member States: Yes: {amountYes}   No: {amountNo}     " +
                        $"Abstain: {amountAbstain}           Population: Yes: {Math.Round(populationYes)}%    No: {Math.Round(populationNo)}%" +
                        $"          Abstain: {Math.Round(populationAbstain)}% ";
                }
            }

            public void GetRequired(TextBox text)
            {
                if((blockingMinority == false) && (membersRequired != 14))
                {
                    text.Text = $"Required For Adoption: Member States: {membersRequired - amountAbstain}       Population: {populationRequired}%";
                }
                else
                {
                    text.Text = $"Required For Adoption: Member States: {membersRequired}       Population: {populationRequired}%";
                }
                
            }
            // The above code displays the requirements for each voting system to the user.
            // This states the number of member states and the population percentage required.

        }
        // The class above is the 'VotingRule' class.


        private void Form1_Load(object sender, EventArgs e)
        {
            ruleBox.Items.Add("Qualified Majority");
            ruleBox.Items.Add("Reinforced Qualified Majority");
            ruleBox.Items.Add("Simple Majority");
            ruleBox.Items.Add("Unanimity");
            ruleBox.SelectedItem = "Qualified Majority";
            currentRule = qualifiedMajority;
            currentRule.GetResult(resultText);
            currentRule.GetRequired(requiredText);
            List<CheckBox> yesCheckboxes = new List<CheckBox> { austriaYes, belgiumYes, bulgariaYes,
            croatiaYes, cyprusYes, czechiaYes, denmarkYes, estoniaYes, finlandYes, franceYes, germanyYes, greeceYes,
            hungaryYes, irelandYes, italyYes, latviaYes, lithuaniaYes, luxembourgYes, maltaYes, netherlandsYes,
            polandYes, portugalYes, romaniaYes, slovakiaYes, sloveniaYes, spainYes, swedenYes};
        }
        // The above code runs when the form laods.




        private void ruleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ruleBox.SelectedItem == "Qualified Majority")
            {
                currentRule = qualifiedMajority;
            }
            else if(ruleBox.SelectedItem == "Reinforced Qualified Majority")
            {
                currentRule = reinforcedQualifiedmajority;
            }
            else if(ruleBox.SelectedItem =="Simple Majority")
            {
                currentRule = simpleMajority;
            }
            else if(ruleBox.SelectedItem == "Unanimity")
            {
                currentRule = unanimity;
            }
            else
            {
                currentRule = qualifiedMajority;
            }
            // The default option when the form loads is this 'Qualified Majority' voting system.
            // The voting system can then be changed by the drop-down selection.

            currentRule.GetResult(resultText);
            currentRule.GetRequired(requiredText);
        }
        // The above code sets which voting system is the current voting system, as dictated by the user.
        // The drop-down menu is available for the user to choose between the different voting systems.




        private void austriaNo_CheckedChanged(object sender, EventArgs e)
        {
            if(austriaNo.Checked == true)
            {
                austria.voteProperty = "NO";
                austriaAbstain.Checked = false;
                austriaYes.Checked = false;
                currentRule.GetResult(resultText);
                // This calculates the result of the vote by calling a method.

                currentRule.GetRequired(requiredText);
                // This displays the requirements that need to be met in order for the vote to pass, by calling a method.

            }
            // If this is changed, the result of the vote is recalculated and displayed to the user.

            else if (austriaYes.Checked == false && austriaNo.Checked == false && austriaAbstain.Checked == false){
                austriaNo.Checked = true;

            }
            // If the user tries to deselect all of the checkbox options for the country, the checkbox is automatically selected.
        }
        // This runs when the state of the checkbox for Austria voting no changes.

        private void austriaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (austriaYes.Checked == true)
            {
                austria.voteProperty = "YES";
                austriaAbstain.Checked = false;
                austriaNo.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if(austriaYes.Checked == false && austriaNo.Checked == false && austriaAbstain.Checked == false){
                austriaYes.Checked = true;

            }

        }

        private void austriaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (austriaAbstain.Checked == true)
            {
                austria.voteProperty = "ABSTAIN";
                austriaNo.Checked = false;
                austriaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (austriaYes.Checked == false && austriaNo.Checked == false && austriaAbstain.Checked == false){ 
                austriaAbstain.Checked = true;

            }
        }


        private void belgiumYes_CheckedChanged(object sender, EventArgs e)
        {
            if (belgiumYes.Checked == true)
            {
                belgium.voteProperty = "YES";
                belgiumAbstain.Checked = false;
                belgiumNo.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (belgiumYes.Checked == false && belgiumNo.Checked == false && belgiumAbstain.Checked == false)
            {
                belgiumYes.Checked = true;

            }
        }

        private void belgiumNo_CheckedChanged(object sender, EventArgs e)
        {
            if (belgiumNo.Checked == true)
            {
                belgium.voteProperty = "NO";
                belgiumAbstain.Checked = false;
                belgiumYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (belgiumYes.Checked == false && belgiumNo.Checked == false && belgiumAbstain.Checked == false)
            {
                belgiumNo.Checked = true;

            }
        }

        private void belgiumAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (belgiumAbstain.Checked == true)
            {
                belgium.voteProperty = "ABSTAIN";
                belgiumNo.Checked = false;
                belgiumYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (belgiumYes.Checked == false && belgiumNo.Checked == false && belgiumAbstain.Checked == false)
            {
                belgiumAbstain.Checked = true;

            }
        }

        private void bulgariaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (bulgariaYes.Checked == true)
            {
                bulgaria.voteProperty = "YES";
                bulgariaNo.Checked = false;
                bulgariaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (bulgariaYes.Checked == false && bulgariaNo.Checked == false && bulgariaAbstain.Checked == false)
            {
                bulgariaYes.Checked = true;

            }
        }

        private void bulgariaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (bulgariaNo.Checked == true)
            {
                bulgaria.voteProperty = "NO";
                bulgariaYes.Checked = false;
                bulgariaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (bulgariaYes.Checked == false && bulgariaNo.Checked == false && bulgariaAbstain.Checked == false)
            {
                bulgariaNo.Checked = true;

            }
        }

        private void bulgariaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (bulgariaAbstain.Checked == true)
            {
                bulgaria.voteProperty = "ABSTAIN";
                bulgariaNo.Checked = false;
                bulgariaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (bulgariaYes.Checked == false && bulgariaNo.Checked == false && bulgariaAbstain.Checked == false)
            {
                bulgariaAbstain.Checked = true;

            }
        }

        private void croatiaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (croatiaYes.Checked == true)
            {
                croatia.voteProperty = "YES";
                croatiaNo.Checked = false;
                croatiaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (croatiaYes.Checked == false && croatiaNo.Checked == false && croatiaAbstain.Checked == false)
            {
                croatiaYes.Checked = true;

            }
        }

        private void croatiaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (croatiaNo.Checked == true)
            {
                croatia.voteProperty = "NO";
                croatiaYes.Checked = false;
                croatiaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (croatiaYes.Checked == false && croatiaNo.Checked == false && croatiaAbstain.Checked == false)
            {
                croatiaNo.Checked = true;

            }
        }

        private void croatiaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (croatiaAbstain.Checked == true)
            {
                croatia.voteProperty = "ABSTAIN";
                croatiaNo.Checked = false;
                croatiaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (croatiaYes.Checked == false && croatiaNo.Checked == false && croatiaAbstain.Checked == false)
            {
                croatiaAbstain.Checked = true;

            }
        }

        private void cyprusYes_CheckedChanged(object sender, EventArgs e)
        {
            if (cyprusYes.Checked == true)
            {
                cyprus.voteProperty = "YES";
                cyprusNo.Checked = false;
                cyprusAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (cyprusYes.Checked == false && cyprusNo.Checked == false && cyprusAbstain.Checked == false)
            {
                cyprusYes.Checked = true;

            }
        }

        private void cyprusNo_CheckedChanged(object sender, EventArgs e)
        {
            if (cyprusNo.Checked == true)
            {
                cyprus.voteProperty = "NO";
                cyprusYes.Checked = false;
                cyprusAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (cyprusYes.Checked == false && cyprusNo.Checked == false && cyprusAbstain.Checked == false)
            {
                cyprusNo.Checked = true;

            }
        }

        private void cyprusAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (cyprusAbstain.Checked == true)
            {
                cyprus.voteProperty = "ABSTAIN";
                cyprusNo.Checked = false;
                cyprusYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (cyprusYes.Checked == false && cyprusNo.Checked == false && cyprusAbstain.Checked == false)
            {
                cyprusAbstain.Checked = true;

            }
        }

        private void czechiaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (czechiaYes.Checked == true)
            {
                czechia.voteProperty = "YES";
                czechiaNo.Checked = false;
                czechiaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (czechiaYes.Checked == false && czechiaNo.Checked == false && czechiaAbstain.Checked == false)
            {
                czechiaYes.Checked = true;

            }
        }

        private void czechiaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (czechiaNo.Checked == true)
            {
                czechia.voteProperty = "NO";
                czechiaYes.Checked = false;
                czechiaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (czechiaYes.Checked == false && czechiaNo.Checked == false && czechiaAbstain.Checked == false)
            {
                czechiaNo.Checked = true;

            }
        }

        private void czechiaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (czechiaAbstain.Checked == true)
            {
                czechia.voteProperty = "ABSTAIN";
                czechiaNo.Checked = false;
                czechiaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (czechiaYes.Checked == false && czechiaNo.Checked == false && czechiaAbstain.Checked == false)
            {
                czechiaAbstain.Checked = true;

            }
        }

        private void denmarkYes_CheckedChanged(object sender, EventArgs e)
        {
            if (denmarkYes.Checked == true)
            {
                denmark.voteProperty = "YES";
                denmarkNo.Checked = false;
                denmarkAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (denmarkYes.Checked == false && denmarkNo.Checked == false && denmarkAbstain.Checked == false)
            {
                denmarkYes.Checked = true;

            }
        }

        private void denmarkNo_CheckedChanged(object sender, EventArgs e)
        {
            if (denmarkNo.Checked == true)
            {
                denmark.voteProperty = "NO";
                denmarkYes.Checked = false;
                denmarkAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (denmarkYes.Checked == false && denmarkNo.Checked == false && denmarkAbstain.Checked == false)
            {
                denmarkNo.Checked = true;

            }
        }

        private void denmarkAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (denmarkAbstain.Checked == true)
            {
                denmark.voteProperty = "ABSTAIN";
                denmarkNo.Checked = false;
                denmarkYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (denmarkYes.Checked == false && denmarkNo.Checked == false && denmarkAbstain.Checked == false)
            {
                denmarkAbstain.Checked = true;

            }
        }

        private void estoniaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (estoniaYes.Checked == true)
            {
                estonia.voteProperty = "YES";
                estoniaNo.Checked = false;
                estoniaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (estoniaYes.Checked == false && estoniaNo.Checked == false && estoniaAbstain.Checked == false)
            {
                estoniaYes.Checked = true;

            }
        }

        private void estoniaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (estoniaNo.Checked == true)
            {
                estonia.voteProperty = "NO";
                estoniaYes.Checked = false;
                estoniaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (estoniaYes.Checked == false && estoniaNo.Checked == false && estoniaAbstain.Checked == false)
            {
                estoniaNo.Checked = true;

            }
        }

        private void estoniaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (estoniaAbstain.Checked == true)
            {
                estonia.voteProperty = "ABSTAIN";
                estoniaNo.Checked = false;
                estoniaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (estoniaYes.Checked == false && estoniaNo.Checked == false && estoniaAbstain.Checked == false)
            {
                estoniaAbstain.Checked = true;

            }
        }

        private void finlandYes_CheckedChanged(object sender, EventArgs e)
        {
            if (finlandYes.Checked == true)
            {
                finland.voteProperty = "YES";
                finlandNo.Checked = false;
                finlandAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (finlandYes.Checked == false && finlandNo.Checked == false && finlandAbstain.Checked == false)
            {
                finlandYes.Checked = true;

            }
        }

        private void finlandNo_CheckedChanged(object sender, EventArgs e)
        {
            if (finlandNo.Checked == true)
            {
                finland.voteProperty = "NO";
                finlandYes.Checked = false;
                finlandAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (finlandYes.Checked == false && finlandNo.Checked == false && finlandAbstain.Checked == false)
            {
                finlandNo.Checked = true;

            }
        }

        private void finlandAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (finlandAbstain.Checked == true)
            {
                finland.voteProperty = "ABSTAIN";
                finlandNo.Checked = false;
                finlandYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (finlandYes.Checked == false && finlandNo.Checked == false && finlandAbstain.Checked == false)
            {
                finlandAbstain.Checked = true;

            }
        }

        private void franceYes_CheckedChanged(object sender, EventArgs e)
        {
            if (franceYes.Checked == true)
            {
                france.voteProperty = "YES";
                franceNo.Checked = false;
                franceAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (franceYes.Checked == false && franceNo.Checked == false && franceAbstain.Checked == false)
            {
                franceYes.Checked = true;

            }
        }

        private void franceNo_CheckedChanged(object sender, EventArgs e)
        {
            if (franceNo.Checked == true)
            {
                france.voteProperty = "NO";
                franceYes.Checked = false;
                franceAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (franceYes.Checked == false && franceNo.Checked == false && franceAbstain.Checked == false)
            {
                franceNo.Checked = true;

            }
        }

        private void franceAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (franceAbstain.Checked == true)
            {
                france.voteProperty = "ABSTAIN";
                franceNo.Checked = false;
                franceYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (franceYes.Checked == false && franceNo.Checked == false && franceAbstain.Checked == false)
            {
                franceAbstain.Checked = true;

            }
        }

        private void germanyYes_CheckedChanged(object sender, EventArgs e)
        {
            if (germanyYes.Checked == true)
            {
                germany.voteProperty = "YES";
                germanyNo.Checked = false;
                germanyAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (germanyYes.Checked == false && germanyNo.Checked == false && germanyAbstain.Checked == false)
            {
                germanyYes.Checked = true;

            }
        }

        private void germanyNo_CheckedChanged(object sender, EventArgs e)
        {
            if (germanyNo.Checked == true)
            {
                germany.voteProperty = "NO";
                germanyYes.Checked = false;
                germanyAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (germanyYes.Checked == false && germanyNo.Checked == false && germanyAbstain.Checked == false)
            {
                germanyNo.Checked = true;

            }
        }

        private void germanyAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (germanyAbstain.Checked == true)
            {
                germany.voteProperty = "ABSTAIN";
                germanyNo.Checked = false;
                germanyYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (germanyYes.Checked == false && germanyNo.Checked == false && germanyAbstain.Checked == false)
            {
                germanyAbstain.Checked = true;

            }
        }

        private void greeceYes_CheckedChanged(object sender, EventArgs e)
        {
            if (greeceYes.Checked == true)
            {
                greece.voteProperty = "YES";
                greeceNo.Checked = false;
                greeceAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (greeceYes.Checked == false && greeceNo.Checked == false && greeceAbstain.Checked == false)
            {
                greeceYes.Checked = true;

            }
        }

        private void greeceNo_CheckedChanged(object sender, EventArgs e)
        {
            if (greeceNo.Checked == true)
            {
                greece.voteProperty = "NO";
                greeceYes.Checked = false;
                greeceAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (greeceYes.Checked == false && greeceNo.Checked == false && greeceAbstain.Checked == false)
            {
                greeceNo.Checked = true;

            }
        }

        private void greeceAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (greeceAbstain.Checked == true)
            {
                greece.voteProperty = "ABSTAIN";
                greeceNo.Checked = false;
                greeceYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (greeceYes.Checked == false && greeceNo.Checked == false && greeceAbstain.Checked == false)
            {
                greeceAbstain.Checked = true;

            }
        }

        private void hungaryYes_CheckedChanged(object sender, EventArgs e)
        {
            if (hungaryYes.Checked == true)
            {
                hungary.voteProperty = "YES";
                hungaryNo.Checked = false;
                hungaryAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (hungaryYes.Checked == false && hungaryNo.Checked == false && hungaryAbstain.Checked == false)
            {
                hungaryYes.Checked = true;

            }
        }

        private void hungaryNo_CheckedChanged(object sender, EventArgs e)
        {
            if (hungaryNo.Checked == true)
            {
                hungary.voteProperty = "NO";
                hungaryYes.Checked = false;
                hungaryAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (hungaryYes.Checked == false && hungaryNo.Checked == false && hungaryAbstain.Checked == false)
            {
                hungaryNo.Checked = true;

            }
        }

        private void hungaryAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (hungaryAbstain.Checked == true)
            {
                hungary.voteProperty = "ABSTAIN";
                hungaryNo.Checked = false;
                hungaryYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (hungaryYes.Checked == false && hungaryNo.Checked == false && hungaryAbstain.Checked == false)
            {
                hungaryAbstain.Checked = true;

            }
        }

        private void irelandYes_CheckedChanged(object sender, EventArgs e)
        {
            if (irelandYes.Checked == true)
            {
                ireland.voteProperty = "YES";
                irelandNo.Checked = false;
                irelandAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (irelandYes.Checked == false && irelandNo.Checked == false && irelandAbstain.Checked == false)
            {
                irelandYes.Checked = true;

            }
        }

        private void irelandNo_CheckedChanged(object sender, EventArgs e)
        {
            if (irelandNo.Checked == true)
            {
                ireland.voteProperty = "NO";
                irelandYes.Checked = false;
                irelandAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (irelandYes.Checked == false && irelandNo.Checked == false && irelandAbstain.Checked == false)
            {
                irelandNo.Checked = true;

            }
        }

        private void irelandAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (irelandAbstain.Checked == true)
            {
                ireland.voteProperty = "ABSTAIN";
                irelandNo.Checked = false;
                irelandYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (irelandYes.Checked == false && irelandNo.Checked == false && irelandAbstain.Checked == false)
            {
                irelandAbstain.Checked = true;

            }
        }

        private void italyYes_CheckedChanged(object sender, EventArgs e)
        {
            if (italyYes.Checked == true)
            {
                italy.voteProperty = "YES";
                italyNo.Checked = false;
                italyAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (italyYes.Checked == false && italyNo.Checked == false && italyAbstain.Checked == false)
            {
                italyYes.Checked = true;

            }
        }

        private void italyNo_CheckedChanged(object sender, EventArgs e)
        {
            if (italyNo.Checked == true)
            {
                italy.voteProperty = "NO";
                italyYes.Checked = false;
                italyAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (italyYes.Checked == false && italyNo.Checked == false && italyAbstain.Checked == false)
            {
                italyNo.Checked = true;

            }
        }

        private void italyAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (italyAbstain.Checked == true)
            {
                italy.voteProperty = "ABSTAIN";
                italyNo.Checked = false;
                italyYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (italyYes.Checked == false && italyNo.Checked == false && italyAbstain.Checked == false)
            {
                italyAbstain.Checked = true;

            }
        }

        private void latviaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (latviaYes.Checked == true)
            {
                latvia.voteProperty = "YES";
                latviaNo.Checked = false;
                latviaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (latviaYes.Checked == false && latviaNo.Checked == false && latviaAbstain.Checked == false)
            {
                latviaYes.Checked = true;

            }
        }

        private void latviaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (latviaNo.Checked == true)
            {
                latvia.voteProperty = "NO";
                latviaYes.Checked = false;
                latviaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (latviaYes.Checked == false && latviaNo.Checked == false && latviaAbstain.Checked == false)
            {
                latviaNo.Checked = true;

            }
        }

        private void latviaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (latviaAbstain.Checked == true)
            {
                latvia.voteProperty = "ABSTAIN";
                latviaNo.Checked = false;
                latviaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (latviaYes.Checked == false && latviaNo.Checked == false && latviaAbstain.Checked == false)
            {
                latviaAbstain.Checked = true;

            }
        }

        private void lithuaniaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (lithuaniaYes.Checked == true)
            {
                lithuania.voteProperty = "YES";
                lithuaniaNo.Checked = false;
                lithuaniaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (lithuaniaYes.Checked == false && lithuaniaNo.Checked == false && lithuaniaAbstain.Checked == false)
            {
                lithuaniaYes.Checked = true;

            }
        }

        private void lithuaniaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (lithuaniaNo.Checked == true)
            {
                lithuania.voteProperty = "NO";
                lithuaniaYes.Checked = false;
                lithuaniaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (lithuaniaYes.Checked == false && lithuaniaNo.Checked == false && lithuaniaAbstain.Checked == false)
            {
                lithuaniaNo.Checked = true;

            }
        }

        private void lithuaniaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (lithuaniaAbstain.Checked == true)
            {
                lithuania.voteProperty = "ABSTAIN";
                lithuaniaNo.Checked = false;
                lithuaniaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (lithuaniaYes.Checked == false && lithuaniaNo.Checked == false && lithuaniaAbstain.Checked == false)
            {
                lithuaniaAbstain.Checked = true;

            }
        }

        private void luxembourgYes_CheckedChanged(object sender, EventArgs e)
        {
            if (luxembourgYes.Checked == true)
            {
                luxembourg.voteProperty = "YES";
                luxembourgNo.Checked = false;
                luxembourgAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (luxembourgYes.Checked == false && luxembourgNo.Checked == false && luxembourgAbstain.Checked == false)
            {
                luxembourgYes.Checked = true;

            }
        }

        private void luxembourgNo_CheckedChanged(object sender, EventArgs e)
        {
            if (luxembourgNo.Checked == true)
            {
                luxembourg.voteProperty = "NO";
                luxembourgYes.Checked = false;
                luxembourgAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (luxembourgYes.Checked == false && luxembourgNo.Checked == false && luxembourgAbstain.Checked == false)
            {
                luxembourgNo.Checked = true;

            }
        }

        private void luxembourgAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (luxembourgAbstain.Checked == true)
            {
                luxembourg.voteProperty = "ABSTAIN";
                luxembourgNo.Checked = false;
                luxembourgYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (luxembourgYes.Checked == false && luxembourgNo.Checked == false && luxembourgAbstain.Checked == false)
            {
                luxembourgAbstain.Checked = true;

            }
        }

        private void maltaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (maltaYes.Checked == true)
            {
                malta.voteProperty = "YES";
                maltaNo.Checked = false;
                maltaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (maltaYes.Checked == false && maltaNo.Checked == false && maltaAbstain.Checked == false)
            {
                maltaYes.Checked = true;

            }
        }

        private void maltaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (maltaNo.Checked == true)
            {
                malta.voteProperty = "NO";
                maltaYes.Checked = false;
                maltaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (maltaYes.Checked == false && maltaNo.Checked == false && maltaAbstain.Checked == false)
            {
                maltaNo.Checked = true;

            }
        }

        private void maltaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (maltaAbstain.Checked == true)
            {
                malta.voteProperty = "ABSTAIN";
                maltaNo.Checked = false;
                maltaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (maltaYes.Checked == false && maltaNo.Checked == false && maltaAbstain.Checked == false)
            {
                maltaAbstain.Checked = true;

            }
        }

        private void netherlandsYes_CheckedChanged(object sender, EventArgs e)
        {
            if (netherlandsYes.Checked == true)
            {
                netherlands.voteProperty = "YES";
                netherlandsNo.Checked = false;
                netherlandsAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (netherlandsYes.Checked == false && netherlandsNo.Checked == false && netherlandsAbstain.Checked == false)
            {
                netherlandsYes.Checked = true;

            }
        }

        private void netherlandsNo_CheckedChanged(object sender, EventArgs e)
        {
            if (netherlandsNo.Checked == true)
            {
                netherlands.voteProperty = "NO";
                netherlandsYes.Checked = false;
                netherlandsAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (netherlandsYes.Checked == false && netherlandsNo.Checked == false && netherlandsAbstain.Checked == false)
            {
                netherlandsNo.Checked = true;

            }
        }

        private void netherlandsAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (netherlandsAbstain.Checked == true)
            {
                netherlands.voteProperty = "ABSTAIN";
                netherlandsNo.Checked = false;
                netherlandsYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (netherlandsYes.Checked == false && netherlandsNo.Checked == false && netherlandsAbstain.Checked == false)
            {
                netherlandsAbstain.Checked = true;

            }
        }

        private void polandYes_CheckedChanged(object sender, EventArgs e)
        {
            if (polandYes.Checked == true)
            {
                poland.voteProperty = "YES";
                polandNo.Checked = false;
                polandAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (polandYes.Checked == false && polandNo.Checked == false && polandAbstain.Checked == false)
            {
                polandYes.Checked = true;

            }
        }

        private void polandNo_CheckedChanged(object sender, EventArgs e)
        {
            if (polandNo.Checked == true)
            {
                poland.voteProperty = "NO";
                polandYes.Checked = false;
                polandAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (polandYes.Checked == false && polandNo.Checked == false && polandAbstain.Checked == false)
            {
                polandNo.Checked = true;

            }
        }

        private void polandAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (polandAbstain.Checked == true)
            {
                poland.voteProperty = "ABSTAIN";
                polandNo.Checked = false;
                polandYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (polandYes.Checked == false && polandNo.Checked == false && polandAbstain.Checked == false)
            {
                polandAbstain.Checked = true;

            }
        }

        private void portugalYes_CheckedChanged(object sender, EventArgs e)
        {
            if (portugalYes.Checked == true)
            {
                portugal.voteProperty = "YES";
                portugalNo.Checked = false;
                portugalAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (portugalYes.Checked == false && portugalNo.Checked == false && portugalAbstain.Checked == false)
            {
                portugalYes.Checked = true;

            }
        }

        private void portugalNo_CheckedChanged(object sender, EventArgs e)
        {
            if (portugalNo.Checked == true)
            {
                portugal.voteProperty = "NO";
                portugalYes.Checked = false;
                portugalAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (portugalYes.Checked == false && portugalNo.Checked == false && portugalAbstain.Checked == false)
            {
                portugalNo.Checked = true;

            }
        }

        private void portugalAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (portugalAbstain.Checked == true)
            {
                portugal.voteProperty = "ABSTAIN";
                portugalNo.Checked = false;
                portugalYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (portugalYes.Checked == false && portugalNo.Checked == false && portugalAbstain.Checked == false)
            {
                portugalAbstain.Checked = true;

            }
        }

        private void romaniaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (romaniaYes.Checked == true)
            {
                romania.voteProperty = "YES";
                romaniaNo.Checked = false;
                romaniaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (romaniaYes.Checked == false && romaniaNo.Checked == false && romaniaAbstain.Checked == false)
            {
                romaniaYes.Checked = true;

            }
        }

        private void romaniaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (romaniaNo.Checked == true)
            {
                romania.voteProperty = "NO";
                romaniaYes.Checked = false;
                romaniaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (romaniaYes.Checked == false && romaniaNo.Checked == false && romaniaAbstain.Checked == false)
            {
                romaniaNo.Checked = true;

            }
        }

        private void romaniaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (romaniaAbstain.Checked == true)
            {
                romania.voteProperty = "ABSTAIN";
                romaniaNo.Checked = false;
                romaniaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (romaniaYes.Checked == false && romaniaNo.Checked == false && romaniaAbstain.Checked == false)
            {
                romaniaAbstain.Checked = true;

            }
        }

        private void slovakiaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (slovakiaYes.Checked == true)
            {
                slovakia.voteProperty = "YES";
                slovakiaNo.Checked = false;
                slovakiaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (slovakiaYes.Checked == false && slovakiaNo.Checked == false && slovakiaAbstain.Checked == false)
            {
                slovakiaYes.Checked = true;

            }
        }

        private void slovakiaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (slovakiaNo.Checked == true)
            {
                slovakia.voteProperty = "NO";
                slovakiaYes.Checked = false;
                slovakiaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (slovakiaYes.Checked == false && slovakiaNo.Checked == false && slovakiaAbstain.Checked == false)
            {
                slovakiaNo.Checked = true;

            }
        }

        private void slovakiaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (slovakiaAbstain.Checked == true)
            {
                slovakia.voteProperty = "ABSTAIN";
                slovakiaNo.Checked = false;
                slovakiaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (slovakiaYes.Checked == false && slovakiaNo.Checked == false && slovakiaAbstain.Checked == false)
            {
                slovakiaAbstain.Checked = true;

            }
        }

        private void sloveniaYes_CheckedChanged(object sender, EventArgs e)
        {
            if (sloveniaYes.Checked == true)
            {
                slovenia.voteProperty = "YES";
                sloveniaNo.Checked = false;
                sloveniaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (sloveniaYes.Checked == false && sloveniaNo.Checked == false && sloveniaAbstain.Checked == false)
            {
                sloveniaYes.Checked = true;

            }
        }

        private void sloveniaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (sloveniaNo.Checked == true)
            {
                slovenia.voteProperty = "NO";
                sloveniaYes.Checked = false;
                sloveniaAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (sloveniaYes.Checked == false && sloveniaNo.Checked == false && sloveniaAbstain.Checked == false)
            {
                sloveniaNo.Checked = true;

            }
        }

        private void sloveniaAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (sloveniaAbstain.Checked == true)
            {
                slovenia.voteProperty = "ABSTAIN";
                sloveniaNo.Checked = false;
                sloveniaYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (sloveniaYes.Checked == false && sloveniaNo.Checked == false && sloveniaAbstain.Checked == false)
            {
                sloveniaAbstain.Checked = true;

            }
        }

        private void spainYes_CheckedChanged(object sender, EventArgs e)
        {
            if (spainYes.Checked == true)
            {
                spain.voteProperty = "YES";
                spainNo.Checked = false;
                spainAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (spainYes.Checked == false && spainNo.Checked == false && spainAbstain.Checked == false)
            {
                spainYes.Checked = true;

            }
        }

        private void spainNo_CheckedChanged(object sender, EventArgs e)
        {
            if (spainNo.Checked == true)
            {
                spain.voteProperty = "NO";
                spainYes.Checked = false;
                spainAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (spainYes.Checked == false && spainNo.Checked == false && spainAbstain.Checked == false)
            {
                spainNo.Checked = true;

            }
        }

        private void spainAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (spainAbstain.Checked == true)
            {
                spain.voteProperty = "ABSTAIN";
                spainNo.Checked = false;
                spainYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (spainYes.Checked == false && spainNo.Checked == false && spainAbstain.Checked == false)
            {
                spainAbstain.Checked = true;

            }
        }

        private void swedenYes_CheckedChanged(object sender, EventArgs e)
        {
            if (swedenYes.Checked == true)
            {
                sweden.voteProperty = "YES";
                swedenNo.Checked = false;
                swedenAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (swedenYes.Checked == false && swedenNo.Checked == false && swedenAbstain.Checked == false)
            {
                swedenYes.Checked = true;

            }
        }

        private void swedenNo_CheckedChanged(object sender, EventArgs e)
        {
            if (swedenNo.Checked == true)
            {
                sweden.voteProperty = "NO";
                swedenYes.Checked = false;
                swedenAbstain.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (swedenYes.Checked == false && swedenNo.Checked == false && swedenAbstain.Checked == false)
            {
                swedenNo.Checked = true;

            }
        }

        private void swedenAbstain_CheckedChanged(object sender, EventArgs e)
        {
            if (swedenAbstain.Checked == true)
            {
                sweden.voteProperty = "ABSTAIN";
                swedenNo.Checked = false;
                swedenYes.Checked = false;
                currentRule.GetResult(resultText);
                currentRule.GetRequired(requiredText);
            }
            else if (swedenYes.Checked == false && swedenNo.Checked == false && swedenAbstain.Checked == false)
            {
                swedenAbstain.Checked = true;

            }
        }

        private void resetAllyes_Click(object sender, EventArgs e)
        {

            List<CheckBox> yesCheckboxes = new List<CheckBox> { austriaYes, belgiumYes, bulgariaYes,
            croatiaYes, cyprusYes, czechiaYes, denmarkYes, estoniaYes, finlandYes, franceYes, germanyYes, greeceYes,
            hungaryYes, irelandYes, italyYes, latviaYes, lithuaniaYes, luxembourgYes, maltaYes, netherlandsYes,
            polandYes, portugalYes, romaniaYes, slovakiaYes, sloveniaYes, spainYes, swedenYes};
            for (int i = 0; i< allCountriesparticipating.Count; i++)
            {
                allCountriesparticipating[i].voteProperty = "YES";
                yesCheckboxes[i].Checked = true;
            }
            // For every country in this list, the "YES" vote option is selected and set to "true".
        }
        // The above code is available for the user to reset all of the options to the default value of "YES".

        
    }
}

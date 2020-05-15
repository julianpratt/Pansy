using System;

class Pansy {

  static void Main() {
    bool Landed;
    bool Manual;
    string input;
    double A, T, E, F;

    // Introduce ourselves
    Messages(1);
    input = UI.Input();
    if ( String.Compare( input.ToUpper() , "YES".Substring(0,input.Length)) == 0 ) Help();

    // Initialise the Calc Engine
    FlightCalcs Calc = new FlightCalcs();

    UI.Output("Altitude   Speed    Range  Deviation  Bearing GlidePath\n");
    UI.Output("  metres   km/hr   metres     metres  degrees   degrees\n");

    // Main Loop
    Landed=false;
    Manual=true;
    A=0.0;
    T=0.0;
    E=0.0;
    F=0.0;
    while (Landed == false) {
      UI.Output(Calc.Output() + "  ? ");
      input = UI.Input();

      if (input.ToUpper() == "H") Help(); 
      if (input.ToUpper() == "X") break; 

      else if (Manual) {
        if ( input.ToUpper() == "A" ) {
          // Engage Autopilot
          Manual=false;
          Calc.CalcStep(A, T, E, F);
          }
        else {
          if (input == null || input.Length == 0) Messages(7);
          else
          {
            string[] fields = input.Split(',');
            if (fields.Length != 4) Messages(7);
            else {
              A=ParsePercent(fields[0], "Ailerons");
              T=ParsePercent(fields[1], "Thrust"  );
              E=ParsePercent(fields[2], "Elevator");
              F=ParsePercent(fields[3], "Flaps"   );
              if ( Calc.Arrived() && A==0.0 && T==0.0 && E==0.0 && F==0.0 ) Landed=true;
              else Calc.CalcStep(A, T, E, F);
              }
            }
          }
        }

      else {
        switch ( input.ToUpper() ) {
          case "A":
            // Autopilot still engaged, so repeat calc step
            Calc.CalcStep(A, T, E, F);
          break;
          case "P":
            // Parachute
            if (Calc.Q > 250.0) Messages(3);
            else                Messages(2);
            Landed=true;
          break;
          default:
            // Disengage autopilot
            Manual=true;
            Messages(5);
          break;
          }
        }
      // If we've crash landed, then we need to break the loop
      if (Calc.crash.Length > 0) {
        Landed=true;
        UI.Output(Calc.crash + "\n\n");
        }
      }



    }

  static double ParsePercent(string item, string ControlType ) {
    double dReturn;

    dReturn = Convert.ToDouble(item);
    if ( dReturn > 100.0 ) {
      UI.Output("                                                 ? Maximum " + ControlType + " is 100%\n");
      dReturn = 100.0;
      }
    if ( dReturn < -100.0 ) {
      UI.Output("                                                 ? Minimum " + ControlType + " is -100%\n");
      dReturn = -100.0;
      }

    return dReturn;
    }

  static void Messages(int MessageId) {
    // Bung out a set of standard messages
    string Message;

    switch (MessageId) {
      case 1:
        Message="PROGRAM PANSY\n\n";
        Message+="You are going to fly a Tristar from one airfield to another.\n";
        Message+="Would you like some flying instruction?\n";
      break;
      case 2:
        Message="Your parachute will not be much use at this height\n";
        Message+="But as you have jumped anyway . . . .\n";
        Message+="                     *SPLAT*\n";
      break;
      case 3:
        Message="Sorry, parachute unavailable\nBut as you have jumped anyway . . . .\n";
        Message+="WHEEEEE\n";
        Message+="         EE\n";
        Message+="                 EE\n";
        Message+="                    EE\n";
        Message+="                      EE\n";
        Message+="                       EE\n";
        Message+="                       EE\n";
        Message+="                     *SPLAT*\n";
      break;
      case 4:
        Message="                                                 ? Auto Pilot Engaged\n";
      break;
      case 5:
        Message="Auto Pilot Disengaged";
      break;
      case 6:
        Message="No reverse thrust in flight";
      break;
      case 7:
        Message="Enter: Ailerons, Thrust, Elevators, Flaps\n";
      break;
      default:
        Message="";
      break;
      }
    UI.Output(Message);
    }

  static void Help() {
    // Give the user help

    UI.Output("YOU ARE ASKED TO INPUT COMMANDS TO THE CONTROLS, SEPARATED BY COMMAS.\n");
    UI.Output("       AILERONS  - CHANGE THE BEARING AND THEREFORE THE DEVIATION\n");
    UI.Output("       THRUST    - INCREASES SPEED\n");
    UI.Output("       ELEVATORS - INCREASE ALTITUDE\n");
    UI.Output("       FLAPS     - SLOW AIRCRAFT  ALSO INCREASE ALTITUDE\n");
    UI.Output("       E.G.                                   ? -50,25,30,-30\n");
    UI.Output("THE VALUES YOU INPUT ARE PERCENTAGES OF THE CONTROL.\n");
    UI.Output("THE OBJECT IS TO FLY THE TRISTAR TO THE OTHER AIRFIELD,\n");
    UI.Output("LAND IT ON THE RUNWAY, BRING IT TO A HALT (E,G. BY REVERSE THRUST),\n");
    UI.Output("AND THEN SET ALL THE CONTROLS TO ZERO.\n");
    UI.Output("THIS CAN ONLY BE DONE WHEN AN ASTERISK APPEARS BESIDE\n");
    UI.Output("THE INPUT QUERY - I.E.                              *?\n\n");

    UI.Output("THE AIRFIELD FROM WHICH YOU TAKE OFF IS 20KM FROM YOUR TARGET AIRFIELD,\n");
    UI.Output("AND ALSO AT RIGHT ANGLES TO IT.\n");
    UI.Output("THE RUNWAY IS 1400 METRES LONG (I.E.INDICATED DEVIATION IS 3850M. AT END)\n");
    UI.Output("AND 80 METRES WIDE.\n");
    UI.Output("THE TARGET AIRFIELD HAS A RUNWAY 1800 METRES LONG AND 80 METRES WIDE.\n");
    UI.Output("THE PERFECT POSITION TO LAND IS WHERE RANGE AND DEVIATION ARE BOTH ZERO.\n\n");

    UI.Output("STALLING SPEED IS ABOUT 200 KM/HR\n\n");

    UI.Output("THE AUTO PILOT LOCKS THE CONTROLS\n");
    UI.Output("IT IS SWITCHED ON BY THRUST=250\n");
    UI.Output("          E.G.                        ? 25,0,50,-25\n");
    UI.Output("                                      ? 0,250,0,0\n");
    UI.Output("WOULD LOCK THE CONTROLS ON ELEV. 25, THRUST  ETC\n");
    UI.Output("TO RETAIN INPUT 'A' - ANY OTHER CHARACTER WOULD SWITCH IT OFF\n");
    UI.Output("         E.G.                         ? A\n");
    UI.Output("                                      ? B\n");
    UI.Output("                                      ? AUTO PILOT DISENGAGED\n\n");

    UI.Output("*PARACHUTE* IS A GET OUT CLAUSE.\n");
    UI.Output("TO ASK FOR IT, SWITCH ON AUTO PILOT AND TYPE 'P'\n\n");

    UI.Output("TO LAND SUCCESSFULLY ON A RUNWAY, GLIDE PATH MUST BE >-3\n\n");

    UI.Output("DISTANCES ARE IN METRES\n");
    UI.Output("SPEED IS IN KM/HR\n");
    UI.Output("ANGLES ARE IN DEGREES\n");
    UI.Output("EACH CYCLE IS 5 SECS\n");
    }

  }


class UI {

  static public void Output(string Message) {
    Console.Write(Message);
    }

  static public string Input() {
    return Console.ReadLine();
    }

  }


class Trig {

  static public double RadDeg (double radians) {
    // Convert a value in radians to degrees.
    return radians * 180.0 / 3.141592659;
    }

  static public double DegRad (double degrees) {
    // Convert a value in degrees to radians.
    return degrees * 3.141592659 / 180.0;
    }

  }

class FlightCalcs {

  // Outputs (exposed as properties and methods)
  public double Q; // Altitude
  public double V; // Speed
  public double Y; // Range
  public double X; // Deviation

  public double M () {
    // Bearing in Degrees.
    return Trig.RadDeg(B);
    }

  public double W () {
    // Glide-Path in Degrees.
    return Trig.RadDeg(C);
    }

  public string warning;
  public string crash = "";

  public bool Arrived () {
    // Alert to whether the the plane has arrived.
    bool bReturn;
    
    bReturn=false;
    if (T5 == 1) bReturn = true;
    return bReturn;
    }

  // In addition the following values are retained for each iteration
  private double B; // Bearing (in radians)
  private double C; // Glide-Path (in radians)
  private int T3;   // Flag to record whether we are on the runway or not
  //private int T4;   // Flag to record that we have taken off once
  private int T5;   // Flag to record that we have landed at the right runway

  public FlightCalcs () {
    // Set the flight starting point
    Q=0.0;               // Altitude
    V=0.0;               // Speed
    Y=20000.0;           // Range
    X=2500.0;            // Deviation
    B=Trig.DegRad(90.0); // Bearing (90 degrees)
    C=0.0;               // Glide Path
    T3=1;                // On the runway
    //T4=0;                // Haven't yet ever taken off
    T5=0;                // Arrival flag
    //J=0
    //I8=0
    //H=100
    }

  public void CalcStep ( double A, double T, double E, double F ) {
    // Main calculation engine

    // Inputs:
    // A = Ailerons
    // T = Thrust
    // E = Elevators
    // F = Flaps

    // Temporary variables
    double PI;
    double D;
    double G;
    double N;
    double S;

    PI=3.141592659;
    D=A*PI/670.0;
    if (Math.Abs(V) <= 50.0) D=D*Math.Abs(V/50.0);
    B=B+D;
    G=(PI/180.0)*(V/20.0+E/10.0+F/30.0-10.0);
    C=C+G;
    if (T3 == 1 && C <= 0.0) C=0.0; // Set glide path to zero when the plane is on the tarmac
    N=T/20.0-9.8122*Math.Sin(C)-V*(Math.Abs(F)/5000.0+0.025);
    S=V*5.0+12.5*N;
    X=X+S*Math.Sin(B)*Math.Cos(C);
    Y=Y-S*Math.Cos(B)*Math.Cos(C);
    Q=Q+S*Math.Sin(C);
    V=V+5.0*N;
    if ((B+PI) <= 0.0001) B=B+2.0*PI;
    if ((B-PI) >= 0.0001) B=B-2.0*PI;

    //Set warning (and adjust if necessary)
    if (T3 == 1 ) {
      // On tarmac, so no warning
      warning="";
      }
    else if ( V > 55.0) {
      // Speed high enough, so no warning
      warning="";
      }
    else if ( V < 0.0 ) {
      // This is really bad!
      warning="You are flying backwards!!";
      C=C-Math.Sin(C);
      Q=Q-237;
      //H=H-2;
      V=V-2*Math.Sin(C);
      }
    else {
      // Just a bit too slow
      warning="You have stalled";
      //H=H-1;
      C=C-0.4*Math.Sin(C);
      Q=Q-53;
      }

    // Adjust glide path, just in case he decides to loop the loop!
    if ( (C+PI) <= -0.0001) {
      V=V-2.0*Math.Sin(C);
      C=C+2.0*PI;
      }
    if ( (C-PI) >= 0.0001) {
      C=C-2.0*PI;
      }

    //if (Q > 0.5) T4=1;  // We have lift off!

    T3=0;
    T5=0;
    crash="";
    if (Q <= 0.5) {
      // Altitude zero situation!
      // Check if plane is on a runway
      Q=0.0;
      if (Y > 20040.0) crash="beyond";
      else if (Y < 19960.0) {
        if (Y > 900.0) {
          crash="beyond";
          }
        else if (Y < -900.0) {
          crash="before";
          }
        else if (Math.Abs(X) > 40.0) {
          crash="off";
          }
        }
      else {
       if (Math.Abs(X-3150.0) > 700.0) {
         crash="off";
         }
       }
      if ( crash.Length > 0 ) {
        //J=J+Math.Abs(Y/492.87-3.0)+Math.Abs(X/300.0-2.0);
        crash="You have crashed " + crash + " the runway ";
        if (V < 0.0) {
          crash = crash + "backwards";
          }
        else if (V < 55.0) {
          crash = crash + "too slow";
          //J=J-Q/5.0+11.0;
          }
        else if (V > 70.0) {
          crash = crash + "too fast";
          //J=J+Q/7.5;
          }
        }
      else {
        T3=1;
        if (Math.Abs(W()*V) > 300.0) {
          //J=J+W*V/50.0;
          crash="You have crashed on the runway, your approach was too steep.";
          }
        else {
          C=0.0;
          if (Math.Abs(V) <= 5.999) {
            T5=1;
            V=0.0;
            X=X-S*Math.Sin(B)*Math.Cos(C);
            Y=Y+S*Math.Cos(B)*Math.Cos(B);
            }
          }
        }
      }
    }

  public string Output () {
    // Give the output as a string
    // In the order: Altitude, Speed, Range, Deviation, Bearing, GlidePath
    return String.Format(" {0,7:#####0} {1,7:#####0}  {2,7:#####0}    {3,7:#####0}  {4,7:#####0}   {5,7:#####0}", Q, V, Y, X, M(), W());
    }
  }



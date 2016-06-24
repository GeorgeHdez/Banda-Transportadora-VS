const int M1_0 = 53;
const int M1_1 = 52;
const int M2_0 = 51;
const int M2_1 = 50;
const int M3_0 = 49;
const int M3_1 = 48;
const int M4_0 = 47;
const int M4_1 = 46;

unsigned char enviar[4];

 
  
void setup() {
  // put your setup code here, to run once:
  pinMode(53,OUTPUT);
  pinMode(52,OUTPUT);
  pinMode(51,OUTPUT);
  pinMode(50,OUTPUT);
  pinMode(49,OUTPUT);
  pinMode(48,OUTPUT);
  pinMode(47,OUTPUT);
  pinMode(46,OUTPUT);
  Serial.begin(9600);
  enviar[0] = 1;
  enviar[1] = 1;
  enviar[2] = 10;
  enviar[3] = 8;
  

}

void loop() {
  // put your main code here, to run repeatedly:
  
   if(Serial.available())
   {
    unsigned char a = Serial.read();
    if(a == 's')
    {

     Serial.write(enviar[0]);  

    Serial.write(enviar[1]);
  
    Serial.write(enviar[2]);
  
    Serial.write(enviar[3]);
    }
   }
  
  

}
void Move_Motor(int ID, int msecond, int dir)
{
  switch(ID)
  {
    case 1: // MOVER MOTOR 1 
    {
      if(dir == 1) // DERECHA
      {
        digitalWrite(M1_1,LOW);
        digitalWrite(M1_0,HIGH);
        delay(msecond);
        digitalWrite(M1_0,LOW);
        Serial.println("mover motor 1 derecha");
      }
      else if(dir == 2) //  IZQUIERDA 
      {
        digitalWrite(M1_0,LOW);
        digitalWrite(M1_1,HIGH);
        delay(msecond);
        digitalWrite(M1_1,LOW);
        Serial.println("mover motor 1 izquierda");
      }
      break;
    }
    case 2: // MOVER MOTOR 2
    {
      if(dir == 1) // DERECHA
      {
        
      }
      else if(dir == 2) //  IZQUIERDA 
      {
        
      }
      break;
    }
    case 3: // MOVER MOTOR 3
    {
      if(dir == 1) // DERECHA
      {
        
      }
      else if(dir == 2) //  IZQUIERDA 
      {
        
      }
      break;
    }
    case 4: //  MOVER MOTOR 4
    {
      if(dir == 1) // DERECHA
      {
        
      }
      else if(dir == 2) //  IZQUIERDA 
      {
        
      }
      break;
    }
    default:
    {
      Serial.print("ERROR");
    }
  }
  
}


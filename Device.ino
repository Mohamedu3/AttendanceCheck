#define GLED 10
#define YLED 11 
#define RLED 12
#include <Adafruit_Fingerprint.h>
#include <SoftwareSerial.h>

String Re = "00";
String _enroll = "0";
String _doEnroll = "0";
String _fingerprint = "0";

uint8_t id;

uint8_t getFingerprintEnroll();

int getFingerprintIDez();

uint8_t getFingerprintEnroll(uint8_t id);

SoftwareSerial mySerial(2,3);
Adafruit_Fingerprint finger = Adafruit_Fingerprint(&mySerial);

void setup()  
{
  pinMode(GLED , OUTPUT);
  pinMode(YLED , OUTPUT);
  pinMode(RLED , OUTPUT);
  
  Serial.begin(9600);
  Serial.println("fingertest");

  finger.begin(57600);  
  if (finger.verifyPassword()) {
    Serial.println("Found fingerprint sensor!");
  }else{
    Serial.println("Did not find fingerprint sensor :(");
    while (1);
  }
}

void loop() {
    if(Serial.available()){
       bt();
       if(Re == "00")
       {
         Re = "0";
         _enroll = "0";
         _doEnroll = "0";
         _fingerprint = "0";
       } 
       else if((Re == "1") && (_enroll == "0"))
       {    
          Serial.print("d");
          _enroll = "1";
          _fingerprint = "1";
       }
       else if((_enroll == "1") && (Re != "00"))
       {
         id = Re.toInt();
         Serial.print("e");
         getFingerprintEnroll();
         _enroll = "0";
         _fingerprint = "0";
       }
       else if(Re == "2" && _fingerprint == "0" && _enroll == "0")
       {
          while(getFingerprintIDez() == -1){
              Serial.print("f");
              if(Serial.available()){
                  bt();
                  if(Re == "00"){
                      break;
                    }
                }
            }
        }
    }
//    else{
//      Serial.print(",");    
//    }
}

void bt()
{
    int commandSize = (int)Serial.read();         
    char command[commandSize];
    int commandPos = 0;
    while(commandPos < commandSize){
        if(Serial.available()) {
            command[commandPos] = (char)Serial.read();
            commandPos++;
        }
    }
         
    command[commandPos] = 0;
    processCommand(command);
}

void processCommand(char* command){
    char * pch;
    pch = strtok (command,"|");
    while (pch != NULL) {
        Re = pch;
        pch = strtok (NULL, "|");
    }
}

int getFingerprintIDez() {
  uint8_t p = finger.getImage();
  if (p != FINGERPRINT_OK)  return -1;

  p = finger.image2Tz();
  if (p != FINGERPRINT_OK)  return -1;

  p = finger.fingerFastSearch();
  if (p != FINGERPRINT_OK)  return -1;
  
  // found a match!
  Serial.print(finger.fingerID);
  return finger.fingerID; 
}

uint8_t getFingerprintEnroll() {
    
  int p = -1;
  Serial.print("g");
  while (p != FINGERPRINT_OK) {
    p = finger.getImage();
    switch (p) {
    case FINGERPRINT_OK:
      Serial.print("h");
      digitalWrite(GLED,HIGH);
      delay(2000);
      break;
    case FINGERPRINT_NOFINGER:
      Serial.print(".");
      digitalWrite(GLED,HIGH);
      delay(500);
      digitalWrite(GLED,LOW);
      break;
    case FINGERPRINT_PACKETRECIEVEERR:
      Serial.println("i");
      break;
    case FINGERPRINT_IMAGEFAIL:
      Serial.println("j");
      break;
    default:
      Serial.println("k");
      break;
    }
  }

  // OK success!

  p = finger.image2Tz(1);
  switch (p) {
    case FINGERPRINT_OK:
      Serial.print("l");
      break;
    case FINGERPRINT_IMAGEMESS:
      Serial.println("m");
      return p;
    case FINGERPRINT_PACKETRECIEVEERR:
      Serial.println("i");
      return p;
    case FINGERPRINT_FEATUREFAIL:
      Serial.println("n");
      return p;
    case FINGERPRINT_INVALIDIMAGE:
      Serial.println("n");
      return p;
    default:
      Serial.println("k");
      return p;
  }
  
  Serial.print("o");
  digitalWrite(GLED,HIGH);
  digitalWrite(GLED,LOW);
  delay(2000);  
  p = 0;
  while (p != FINGERPRINT_NOFINGER) {
    p = finger.getImage();
  }
  Serial.print("p");
  Serial.print(id);
  p = -1;
  Serial.print("q");
  while (p != FINGERPRINT_OK) {
    p = finger.getImage();
    switch (p) {
    case FINGERPRINT_OK:
      Serial.print("h");
      digitalWrite(GLED,HIGH);
      delay(2000);
      break;
    case FINGERPRINT_NOFINGER:
      Serial.print(".");
      digitalWrite(GLED,HIGH);
      delay(500);
      digitalWrite(GLED,LOW);
      break;
    case FINGERPRINT_PACKETRECIEVEERR:
      Serial.println("i");
      break;
    case FINGERPRINT_IMAGEFAIL:
      Serial.println("j");
      break;
    default:
      Serial.println("k");
      break;
    }
  }

  // OK success!

  p = finger.image2Tz(2);
  switch (p) {
    case FINGERPRINT_OK:
      Serial.print("l");
      break;
    case FINGERPRINT_IMAGEMESS:
      Serial.println("m");
      return p;
    case FINGERPRINT_PACKETRECIEVEERR:
      Serial.println("i");
      return p;
    case FINGERPRINT_FEATUREFAIL:
      Serial.println("n");
      return p;
    case FINGERPRINT_INVALIDIMAGE:
      Serial.println("n");
      return p;
    default:
      Serial.println("k");
      return p;
  }
  
  // OK converted!
  Serial.print("f");
  Serial.print(id);
  
  p = finger.createModel();
  if (p == FINGERPRINT_OK) {
    Serial.print("s");
      digitalWrite(GLED,HIGH);
      delay(500);
      digitalWrite(GLED,LOW);
  } else if (p == FINGERPRINT_PACKETRECIEVEERR) {
    Serial.println("i");
    return p;
  } else if (p == FINGERPRINT_ENROLLMISMATCH) {
    Serial.print("t");
    digitalWrite(GLED,LOW);
    digitalWrite(RLED,HIGH);
    delay(500);
    digitalWrite(RLED,LOW);
    return p;
  } else {
    Serial.println("k");
    return p;
  }   
  
  Serial.print("p"); 
  Serial.print(id);
  p = finger.storeModel(id);
  if (p == FINGERPRINT_OK) {
    Serial.print("u");
    digitalWrite(GLED,HIGH);
    delay(2000);
    digitalWrite(GLED,LOW);
  } else if (p == FINGERPRINT_PACKETRECIEVEERR) {
    Serial.println("i");
    return p;
  } else if (p == FINGERPRINT_BADLOCATION) {
    Serial.println("v");
    return p;
  } else if (p == FINGERPRINT_FLASHERR) {
    Serial.println("w");
    return p;
  } else {
    Serial.println("k");
    return p;
  }   
}

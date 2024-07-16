

Text = input("Enter the Text file: ")
import speech_recognition as sr
r = sr.Recognizer()

hellow =sr.AudioFile(Wav)
with hellow as source:
    audio = r.record(source)
#try:
    s = r.recognize_google(audio)
    #print("Text: "+s)
    #Text = input("Enter the path to the Text file: ")
    f = open(Text, "w")
    f.write(s)
    f.close()
import speech_recognition as sr

def AudioText(audioFile):
    recognizer = sr.Recognizer()

    with sr.AudioFile(audioFile) as source:
        print("Say something")
        audio_data = recognizer.record(source)

    try:
        text = recognizer.recognize_google(audio_data, language="he-IL")
        # יוצרת קובץ חדש או מחליפה את התוכן של קובץ קיים
        with open('clientText.txt', 'w', encoding='utf-8') as file:
            file.write(text)
        return (text)

    except sr.UnknownValueError:
        return ("Google Speech Recognition could not understand the audio.")
    except sr.RequestError as e:
        return ("Could not request results from Google Speech Recognition service; {0}".format(e))

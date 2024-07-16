@app.route('/blood_test', methods=['POST'])
def blood_test():
    # קבלת נתוני JSON מהבקשה
    data = request.get_json()
    if(data==''):
        return jsonify({'error': 'No file selected'}), 400

    result= BloodTest.read_pdf(data)
    return jsonify({'message': result}), 200

if __name__ == '__main__':
    app.run(host='localhost', port=5000, debug=True)


def analysis_file(pdf_path):
    # קבלת קובץ PDF והמרתו לתמונה
    scanned_pdf = pdf_path
    reader = PdfReader(scanned_pdf)
    pdf_string_2 = ''
    for page in range(len(reader.pages)):
        pdf_string_2 += reader.pages[page].extract_text()
    doc = fitz.open(pdf_path)
    for page in doc:
        pix = page.get_pixmap()
        path_image = f'{page}.png'
        pix.save(path_image)

    result = {}  # משתנה שיכיל את כל התוצאות של הבדיקות השגויות
    image = cv2.imread(path_image)  # קרא את התמונה
    # הגדר טווח צבעים לצבע אדום ב-HSV
    lower_red = np.array([0, 50, 50])
    upper_red = np.array([10, 255, 255])
    hsv_image = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)  # המרת התמונה לצבעים ב-HSV
    mask = cv2.inRange(hsv_image, lower_red, upper_red)  # פילטר צבע אדום בהתאם לטווח הצבעים שהוגדר
    red_only = cv2.bitwise_and(image, image, mask=mask)  # שמור רק את הצבע האדום מהתמונה המקורית

    # הצג את התמונה שמציגה רק את הצבע האדום
    cv2.imwrite('red_only_image.jpg', red_only)
    new_image = cv2.imread('red_only_image.jpg')

    # החזרת כל השורות שיש בהם פיקסלים אדומים
    red_rows = np.any(red_only, axis=(1, 2))
    num_additional_rows = 6
    red_row_indices = red_rows.nonzero()[0]  # קבלת השורות עם הפיקסלים האדומים
    # בדיקה האם לא נמצאו פיקסלים אדומים
    if len(red_row_indices) == 0:
        return result
    # חישוב מדדי ההתחלה והסוף עבור השורות שנמצאו
    start_index = max(0, red_row_indices[0] - num_additional_rows)
    end_index = min(len(image), red_row_indices[-1] + num_additional_rows + 1)
    rows_with_red_pixels_and_surrounding = image[start_index:end_index, :, :]  # בחר את השורות עם פיקסלים אדומים
    # שמירת תמונה
    cv2.imwrite('errorResult.png', rows_with_red_pixels_and_surrounding)
    line_red = cv2.imread('errorResult.png')

    # המר את התמונה לגווני אפור
    gray_image = cv2.cvtColor(new_image, cv2.COLOR_BGR2GRAY)
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    # עיבוד סופי על מנת שנוכל להשתמש בתמונה כגון הפרדת טקסט
    _, thresh = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)
    # יבוא התקנות של ספריית tesseract
    pytesseract.pytesseract.tesseract_cmd = 'C:\\Program Files\\Tesseract-OCR\\tesseract'
    # הפעל את Tesseract על התמונה וקרא את הטקסט
    line = pytesseract.image_to_string(line_red)

    # מעבר על כל התמונה שהתקבלה לצורך ניתוח כל בדיקה
    all_error_test = line.split('\n\n')  # פרדה כל בדיקה לבדיקה נפרדת
    for test in all_error_test:
        blood_test = test.split()  # הפרדה כל מילה בבדיקה הנוכחית
        name_test = blood_test[0]  # המילה הראשונה מכילה את שם הבדיקה
        print("name test:" + name_test)
        # מציאת ערך הבדיקה
        # שימוש בביטוי רגולרי למציאת מספרים
        numbers = re.findall(r'\d+', test)
        # בדיקה אם נמצאו מספרים, ואז לקחת את האחרון
        if numbers:
            error_test = numbers[-1]
        print("error test: " + error_test)
        # זימון הפונקציה הבודקת האם הבדיקה תקינה
        r = ratingResult(name_test, error_test)
        if (r != None):
            result[name_test] = r
        print(r)
        print('\n')
    return result
def analysis_file(pdf_path):
    #קבלת קובץ PDF והמרתו לתמונה
    scanned_pdf = pdf_path
    reader = PdfReader(scanned_pdf)
    pdf_string_2 = ''
    for page in range(len(reader.pages)):
        pdf_string_2 += reader.pages[page].extract_text()
    print(pdf_string_2)
    doc = fitz.open(pdf_path)
    for page in doc:
        pix = page.get_pixmap()
        path_image = f'{page}.png'
        pix.save(path_image)


    # קרא את התמונה
    image = cv2.imread(path_image)

    # הגדר טווח צבעים לצבע אדום ב-HSV
    lower_red = np.array([0, 50, 50])
    upper_red = np.array([10, 255, 255])

    # המרת התמונה לצבעים ב-HSV
    hsv_image = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)

    # פילטר צבע אדום בהתאם לטווח הצבעים שהוגדר
    mask = cv2.inRange(hsv_image, lower_red, upper_red)

    # שמור רק את הצבע האדום מהתמונה המקורית
    red_only = cv2.bitwise_and(image, image, mask=mask)

    # הצג את התמונה שמציגה רק את הצבע האדום
    cv2.imshow('Red Only', red_only)
    cv2.waitKey(0)
    cv2.destroyAllWindows()

    cv2.imwrite('red_only_image.jpg', red_only)
    new_image = cv2.imread('red_only_image.jpg')

    #......................................................
    # החזרת כל השורות שיש בהם פיקסלים אדומים
    # rows_with_red_pixels = my_image[red_rows, :, :]
    red_rows = np.any(red_only, axis=(1, 2))

    num_additional_rows = 6

    # Get the indices of the rows with red pixels
    red_row_indices = red_rows.nonzero()[0]

    # Calculate the start and end indices for the rows to include
    start_index = max(0, red_row_indices[0] - num_additional_rows)
    end_index = min(len(image), red_row_indices[-1] + num_additional_rows + 1)

    # Select the rows with red pixels and additional rows
    rows_with_red_pixels_and_surrounding = image[start_index:end_index, :, :]
    # .......................................................................................................................

    # שמירת התמונה??????????????????????????????????????????????????????????????????????????????????????????????????????????
    # cv2.imwrite('errorResult.png', rows_with_red_pixels_and_surrounding)

    cv2.imshow("Rows with Red Pixels", rows_with_red_pixels_and_surrounding)
    cv2.waitKey(0)
    cv2.destroyAllWindows()
    #......................................................

    cv2.imwrite('errorResult.png', rows_with_red_pixels_and_surrounding)
    line_red = cv2.imread('errorResult.png')

    # המר את התמונה לגווני אפור
    gray_image = cv2.cvtColor(new_image, cv2.COLOR_BGR2GRAY)

    #################################################################################
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    # Perform thresholding or any preprocessing if necessary
    # For example, you can threshold to separate text from the background
    _, thresh = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)

    ##########################################################################################

    pytesseract.pytesseract.tesseract_cmd = 'C:\\Program Files\\Tesseract-OCR\\tesseract'

    # הפעל את Tesseract על התמונה וקרא את הטקסט
    text = pytesseract.image_to_string(gray_image)
    line = pytesseract.image_to_string(line_red)


    #מעבר על כל התמונה שהתקבלה לצורך ניתוח כל בדיקה
    all_error_test=line.split('\n\n')
    result = {}  # משתנה שיכיל את כל התוצאות של הבדיקות השגויות
    for test in all_error_test:
        print(test)
        blood_test= test.split()
        name_test= blood_test[0]
        print("name test:"+ name_test)
        # שימוש בביטוי רגולרי למציאת מספרים
        numbers = re.findall(r'\d+', test)
        # בדיקה אם נמצאו מספרים, ואז לקחת את האחרון
        if numbers:
            error_test = numbers[-1]
        print("error test: "+ error_test)

        # זימון הפונקציה הבודקת האם הבדיקה תקינה
        r = ratingResult(name_test, error_test)
        if(r!=None):
            result[name_test] = r
        print(r)
        print('\n')
    print(result)
    return result
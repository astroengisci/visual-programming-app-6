﻿Public Class Game
	Dim Correct = 0
	Dim Asked = 1
	Dim NumToAsk = 5

	Dim minutes As Integer
	Dim seconds As Integer
    'Dim TotalMinutes As Integer = 0
    Dim TotalSeconds As Integer = 150

    Dim stopTimeClicked = 0
    Dim addTimeClicked = 0
    Dim help50Clicked = 0
    Dim helpCorrectClicked = 0

    Dim CorrectAnswer = 1
	Dim CorrectAnswerText As String

	Dim Questions As List(Of Question) = New List(Of Question)

	Private Sub CloneQuestionsList()
		For Each q In Title.Questions
			Questions.Add(q.Clone())
		Next

	End Sub

	Sub fixTime()
        If TotalSeconds > 59 Then
            TotalSeconds -= 60
            minutes += 1
            fixTime()
        End If
    End Sub

    'Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click

    Private Sub LoadNewQuestion()
		' Get random question
		Dim rnd = New Random()
		If Questions.Count = 0 Then ' Sanity check
			CloneQuestionsList()
		End If

		Dim Question = Questions(
							rnd.Next(0, Questions.Count - 1))

		Questions.Remove(Question) ' To prevent duplication

		CorrectAnswerText = Question.Correct
		lblQuestion.Text = Question.Prompt
		CorrectAnswer = rnd.Next(1, 5)

		Select Case CorrectAnswer
			Case 1
				btnAnswer1.Text = Question.Correct
				btnAnswer2.Text = Question.Wrong1
				btnAnswer3.Text = Question.Wrong2
				btnAnswer4.Text = Question.Wrong3
			Case 2
				btnAnswer2.Text = Question.Correct
				btnAnswer1.Text = Question.Wrong1
				btnAnswer3.Text = Question.Wrong2
				btnAnswer4.Text = Question.Wrong3
			Case 3
				btnAnswer3.Text = Question.Correct
				btnAnswer2.Text = Question.Wrong1
				btnAnswer1.Text = Question.Wrong2
				btnAnswer4.Text = Question.Wrong3
			Case 4
				btnAnswer4.Text = Question.Correct
				btnAnswer2.Text = Question.Wrong1
				btnAnswer3.Text = Question.Wrong2
				btnAnswer1.Text = Question.Wrong3
		End Select

        'minutes = TotalMinutes
        fixTime()
        seconds = TotalSeconds
        Timer1.Enabled = True
        btnAnswer1.Show()
        btnAnswer2.Show()
        btnAnswer3.Show()
        btnAnswer4.Show()

    End Sub

	Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click

        Dim Answer = 0

        If btnAnswer1.Checked Then
            Answer = 1
        ElseIf btnAnswer2.Checked Then
            Answer = 2
        ElseIf btnAnswer3.Checked Then
            Answer = 3
        ElseIf btnAnswer4.Checked Then
            Answer = 4
        End If

        'No answers were checked
        If Answer = 0 Then
            MessageBox.Show("Please select an answer")
            Return
        End If

        If Answer = CorrectAnswer Then
            MessageBox.Show("Correct!")
            Correct += 1
            Asked += 1
        Else
            MessageBox.Show("Sorry, the correct answer was" & vbCrLf & CorrectAnswerText,
                            "Incorrect")
            Asked += 1
        End If

        If Asked >= NumToAsk Then
            QuitGame()
        End If

        LoadNewQuestion()
        lblScore.Text = "$" + Correct.ToString
    End Sub

    Private Sub QuitGame()
        Score.Show()
        Me.Close()
    End Sub

    Private Sub btnquit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        QuitGame()
    End Sub


    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblScore.Text = "$0"

		CloneQuestionsList()
		If NumToAsk < Questions.Count Then ' Sanity check
            NumToAsk = Questions.Count
        End If
        LoadNewQuestion()

        displayTimer()
        Timer1.Enabled = True
        Timer1.Interval = 1000
    End Sub

    Sub decrement()
        seconds = seconds - 1
        If seconds < 0 Then
            minutes = minutes - 1
            seconds = 59
        End If

        If minutes < 0 Then
            'If an answer is not chosen before time runs out, load new question
            Asked += 1
            LoadNewQuestion()
        End If
        displayTimer()

        If minutes < 0 Then
			Asked += 1
			If Asked >= NumToAsk Then
				QuitGame()
			End If
			LoadNewQuestion()
		End If
		displayTimer()

    End Sub

    Sub displayTimer()
		Dim displaySecond As String
		Dim displayMinute As String
		displaySecond = seconds
		displayMinute = minutes
		If seconds < 10 Then displaySecond = "0" & seconds
		If minutes < 10 Then displayMinute = "0" & minutes
		lblTimer.Text = displayMinute & ":" & displaySecond
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		decrement()
	End Sub

    Private Sub btnHelpStop_Click(sender As Object, e As EventArgs) Handles btnHelpStop.Click
        If stopTimeClicked = 0 Then
            Timer1.Enabled = False
            stopTimeClicked += 1
            btnHelpStop.Text = ""
        End If
    End Sub

    Private Sub btnHelpAdd_Click(sender As Object, e As EventArgs) Handles btnHelpAdd.Click
        If addTimeClicked = 0 Then
            seconds = TotalSeconds
            addTimeClicked += 1
            btnHelpAdd.Text = ""
        End If
    End Sub

    Private Sub btnHelp50_Click(sender As Object, e As EventArgs) Handles btnHelp50.Click
        If help50Clicked = 0 Then
            If CorrectAnswer = 1 Then
                btnAnswer2.Hide()
                btnAnswer3.Hide()
            ElseIf CorrectAnswer = 2 Then
                btnAnswer1.Hide()
                btnAnswer4.Hide()
            ElseIf CorrectAnswer = 3 Then
                btnAnswer1.Hide()
                btnAnswer4.Hide()
            Else
                btnAnswer2.Hide()
                btnAnswer3.Hide()
            End If
            help50Clicked += 1
            btnHelp50.Text = ""
        End If
    End Sub

	Private Sub btnHelpShow_Click(sender As Object, e As EventArgs) Handles btnHelpShow.Click
		If helpCorrectClicked = 0 Then
			If CorrectAnswer = 1 Then
				btnAnswer2.Hide()
				btnAnswer3.Hide()
				btnAnswer4.Hide()
			ElseIf CorrectAnswer = 2 Then
				btnAnswer1.Hide()
				btnAnswer3.Hide()
				btnAnswer4.Hide()
			ElseIf CorrectAnswer = 3 Then
				btnAnswer1.Hide()
				btnAnswer2.Hide()
				btnAnswer4.Hide()
			Else
				btnAnswer1.Hide()
				btnAnswer2.Hide()
				btnAnswer3.Hide()
			End If
			helpCorrectClicked += 1
			btnHelpShow.Text = ""
		End If
	End Sub
End Class
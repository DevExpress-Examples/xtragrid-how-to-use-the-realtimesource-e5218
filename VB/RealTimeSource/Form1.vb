﻿Imports Microsoft.VisualBasic
Imports DevExpress.Data
Imports DevExpress.XtraEditors.Repository
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace RealTimeSourceWinForms
	Partial Public Class Form1
		Inherits Form
		Private Persons As BindingList(Of Data)
		Private Count As Integer = 50
		Private Random As New Random()
		Public Sub New()

			InitializeComponent()
			Persons = New BindingList(Of Data)()
			For i As Integer = 0 To Count - 1
				Persons.Add(New Data With {.Id = i, .Text = "Text" & i, .Progress = GetNumber()})
			Next i


			Dim rts As New RealTimeSource() With {.DataSource = Persons}
			gridControl1.DataSource = rts

			Dim timer As New Timer()
			timer.Interval = 10
			AddHandler timer.Tick, AddressOf Tick
			timer.Start()

			gridView1.Columns("Progress").ColumnEdit = New RepositoryItemProgressBar()
		End Sub
		Private Sub Tick(ByVal sender As Object, ByVal e As EventArgs)
			Dim index As Integer = Random.Next(0, Count)
			Persons(index).Id = GetNumber()
			Persons(index).Text = "Text" & GetNumber()
			Persons(index).Progress = GetNumber()
		End Sub
		Private Function GetNumber() As Integer
			Return Random.Next(0, Count)
		End Function
	End Class
	Public Class Data
		Implements INotifyPropertyChanged
		Private _Id As Integer
		Public _Text As String
		Public _Progress As Double

		Public Property Id() As Integer
			Get
				Return _Id
			End Get
			Set(ByVal value As Integer)
				_Id = value
				NotifyPropertyChanged("Id")
			End Set
		End Property
		Public Property Text() As String
			Get
				Return _Text
			End Get
			Set(ByVal value As String)
				_Text = value
				NotifyPropertyChanged("Text")
			End Set
		End Property
		Public Property Progress() As Double
			Get
				Return _Progress
			End Get
			Set(ByVal value As Double)
				_Progress = value
				NotifyPropertyChanged("Progress")
			End Set
		End Property

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
		Private Sub NotifyPropertyChanged(ByVal name As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
		End Sub
	End Class
End Namespace

Imports System.ComponentModel


Public Class WorkSpaceViewModel

    Dim _cmdP As New ParamCommand(AddressOf EmitMessage)

    Public ReadOnly Property Message As ICommand
        Get
            Return _cmdP
        End Get
    End Property

    Private Sub EmitMessage()
        MsgBox("zz")
    End Sub

    Public Class ParamCommand
        Implements ICommand

        Private _action As Action(Of String)

        Sub New(action As Action(Of String))
            _action = action
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function

        Public Event CanExecuteChanged(sender As Object, e As EventArgs) Implements ICommand.CanExecuteChanged

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            _action(parameter.ToString)
        End Sub
    End Class
End Class

Imports System.Net

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button1.Visible = False
        Try
            TextBox2.Text = GetSSLExpirationDate(TextBox1.Text)
        Catch ex As Exception
            TextBox2.Text = "bad request"
        End Try
        Button1.Visible = True
    End Sub

    Private Function GetSSLExpirationDate(ByVal url As String) As String
        Dim u As New Uri(url)
        Dim sp As ServicePoint = ServicePointManager.FindServicePoint(u)
        Dim groupName As String = Guid.NewGuid().ToString()
        Dim req As HttpWebRequest = TryCast(HttpWebRequest.Create(u), HttpWebRequest)
        req.ConnectionGroupName = groupName
        Using resp As WebResponse = req.GetResponse()
        End Using
        sp.CloseConnectionGroup(groupName)
        Dim expiryDate As String = sp.Certificate.GetExpirationDateString()
        Return expiryDate
    End Function

End Class

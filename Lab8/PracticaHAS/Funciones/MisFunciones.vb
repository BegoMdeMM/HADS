﻿Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.Text
Imports System.Data.SqlClient




Public Class MisFunciones


    Public Function NumConf() As Single
        Randomize()
        NumConf = CLng(Rnd() * 9000000) + 1000000
    End Function

    Public Function SimulacionVerificar(ByVal NumConf As Single) As Boolean
        Dim valor As Single = NumConf
        If (valor Mod 2 = 0) Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub sendMail(ByVal direccion As String, ByVal titulo As String, ByVal mensaje As String)

        Dim SmtpServer As New SmtpClient("smtp.gmail.com", 587)
        'puerto que usa gmail: 465
        SmtpServer.Credentials = New Net.NetworkCredential("hasjosebego@gmail.com", "ijlgkiwksuzhkklm")
        SmtpServer.EnableSsl = True
        Dim mail As New MailMessage("hasjosebego@gmail.com", direccion, titulo, mensaje)
        SmtpServer.Send(mail)
    End Sub


    Shared Function GetMd5Hash(ByVal md5Hash As MD5, ByVal input As String) As String

        ' Convierte la entrada en tipo Byte y computa.
        Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input))

        'Crea una estructura
        Dim sBuilder As New StringBuilder()

        ' Cicla por cada byte del hash y le asigna un valor hexadecimal
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Devuelve el resumen MD5
        Return sBuilder.ToString()

    End Function 'GetMd5Hash

    Function RandomPassGenerator() As String
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 8
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next
        Dim password = sb.ToString
        Return password

    End Function

    Function MediaDedicacionAlumnos(miAsignatura As String) As Double
        Dim media As Double = 0.0
        Dim dreader As SqlDataReader
        Try
            Dim sqlQuery As String = "SELECT AVG(EstudiantesTareas.HReales) AS Media FROM Asignaturas JOIN TareasGenericas ON Asignaturas.codigo = TareasGenericas.CodAsig JOIN EstudiantesTareas ON TareasGenericas.Codigo = EstudiantesTareas.CodTarea WHERE Asignaturas.codigo = '" & miAsignatura & "'"
            Dim conexion As New SqlConnection("Server=tcp:serverlab4.database.windows.net,1433;Database=Lab4;User ID=has17@serverlab4;Password=J0s3B3g0;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
            conexion.Open()
            Dim sqlCommand As New SqlCommand(sqlQuery, conexion)
            dreader = sqlCommand.ExecuteReader()
            While (dreader.Read)
                media = Convert.ToDouble(dreader("Media"))
            End While
            'media = dreader.GetString(0)
            conexion.Close()
        Catch ex As Exception
            MsgBox("Ha ocurrido una excepción")
        End Try
        Return media
    End Function

End Class

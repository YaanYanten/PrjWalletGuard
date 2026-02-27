Imports System.Windows.Forms
Imports WalletGuard.WalletGuard

''' <summary>
''' Punto de entrada de la aplicación.
''' </summary>
Module Program

    <STAThread>
    Sub Main()
        Application.SetHighDpiMode(HighDpiMode.SystemAware)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New FormMain())
    End Sub

End Module

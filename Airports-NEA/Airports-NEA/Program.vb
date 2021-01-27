
Module Module1

    Class Airport
        Public code As String
        Public name As String
        Public departure As Boolean
    End Class

    Class Distance
        Public departure As String
        Public arival As String
        Public distanceKm As Integer
    End Class

    Dim airports As New List(Of Airport)
    Dim distances As New Dictionary(Of String, Dictionary(Of String, Distance))

    Dim selectedAirport As Airport

    Sub LoadInitialAirports()

        airports.Clear()
        distances.Clear()

        Dim lpl As Airport = New Airport()
        lpl.code = "LPL"
        lpl.name = "Liverpool John Lennon"
        lpl.departure = True

        Dim boh As Airport = New Airport()
        boh.code = "BOH"
        boh.name = "Bournemouth International"
        boh.departure = True

        airports.Add(lpl)
        distances.Add(lpl.code, New Dictionary(Of String, Distance))
        airports.Add(boh)
        distances.Add(boh.code, New Dictionary(Of String, Distance))

    End Sub


    Sub Main()

        Call Sub() PrintWelcome()

        Call Sub() LoadInitialAirports()

        Call Sub() LoadCSVAirports()

        Dim selection As String
        Dim finish As Boolean


        'this sets out the menu

        finish = False


        Do While finish = False

            Call Sub() PrintMenu()

            selection = Console.ReadLine

            Select Case selection
                Case 1
                    Call Sub() Airport_details()
                Case 2
                    Call Sub() Flight_details()
                Case 3
                    Call Sub() Price_profit()
                Case 4
                    Call Sub() Clear_data()
                Case 5
                    Call Sub() Quit()
                    finish = True
                Case Else
                    Console.WriteLine("Invalid option: " & selection)
            End Select

        Loop




    End Sub

    Sub PrintWelcome()

        Console.WriteLine("Welcome to the airport cost/benefit analysis system beta")

    End Sub

    Sub PrintMenu()

        Console.WriteLine("Please select one of the options below by typing the number next to it")
        Console.WriteLine("")
        Console.WriteLine("1. Enter airport details")
        Console.WriteLine("2. Enter flight details")
        Console.WriteLine("3. Enter price plan and calculate profit")
        Console.WriteLine("4. Clear data")
        Console.WriteLine("5. Quit")


    End Sub

    Sub Quit()

        Console.WriteLine("Please press enter to quit the program")
        Console.ReadLine()

    End Sub

    Sub Airport_details()

        Dim back As Boolean = False
        Dim code As String

        Do While back = False

            Console.WriteLine("Please enter the three-letter airport code for your departure or 5 to exit to main menu")

            code = Console.ReadLine

            If code = "5" Then
                back = True
            Else

                For i = 1 To airports.Count

                    Dim airport = airports(i - 1)
                    If airport.code.ToLower = code.ToLower Then
                        If airport.departure Then
                            selectedAirport = airport
                            Console.WriteLine("You have selected " & airport.name & " as your start")
                            back = True
                        Else
                            Console.WriteLine("You have selected " & airport.name & " which is invalid departure location")
                        End If

                    End If

                Next
            End If


        Loop

    End Sub
    Sub LoadCSVAirports()

        Using csv As New Microsoft.VisualBasic.FileIO.TextFieldParser(PrompFileToLoad())

            csv.TextFieldType = FileIO.FieldType.Delimited
            csv.SetDelimiters(",")

            Dim count As Integer = 0
            Dim currentRow As String()
            While Not csv.EndOfData
                Try
                    currentRow = csv.ReadFields()

                    Dim airport As New Airport()
                    airport.code = currentRow(0)
                    airport.name = currentRow(1)
                    airport.departure = False

                    airports.Add(airport)

                    Dim distanceLpl As New Distance()
                    distanceLpl.departure = "LPL"
                    distanceLpl.arival = airport.code
                    distanceLpl.distanceKm = Convert.ToInt32(currentRow(2))

                    Dim lplValue As New Dictionary(Of String, Distance)
                    distances.TryGetValue(distanceLpl.departure, lplValue)
                    lplValue.Add(airport.code, distanceLpl)

                    Dim distanceBoh As New Distance()
                    distanceBoh.departure = "BOH"
                    distanceBoh.arival = airport.code
                    distanceBoh.distanceKm = Convert.ToInt32(currentRow(3))

                    Dim bohValue As New Dictionary(Of String, Distance)
                    distances.TryGetValue(distanceBoh.departure, bohValue)
                    bohValue.Add(airport.code, distanceBoh)

                    count += 1

                Catch ex As Microsoft.VisualBasic.
                    FileIO.MalformedLineException
                    Console.WriteLine("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While

            Console.WriteLine("Loaded " & count & " airport data items.")

        End Using

    End Sub

    Function PrompFileToLoad() As String
        Dim file As String = "D:\coding\projects\yr11-GCSE-NEA-project\Airports\Airports-NEA\Airports.txt"
        Dim ask As Boolean = True
        Do While ask
            Console.WriteLine("Please specify location of CSV file with airport data")
            Dim askFile As String = Console.ReadLine()
            If askFile <> "" Then
                file = askFile
            End If
            If FileIO.FileSystem.FileExists(file) Then
                ask = False
            Else
                Console.WriteLine("File " & file & " not found.")
            End If

        Loop
        Return file
    End Function

    Sub Flight_details()

        Console.WriteLine("under construction, flight details")

    End Sub

    Sub Price_profit()

        Console.WriteLine("under construction, price - profit analysis")

    End Sub

    Sub Clear_data()

        Console.WriteLine("under construction, clear data")

    End Sub


End Module
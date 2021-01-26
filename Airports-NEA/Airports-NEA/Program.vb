
Module Module1

    Class Airport
        Public code As String
        Public name As String
        Public departure As Boolean
        Public distance1 As Integer
        Public distance2 As Integer
    End Class

    Dim airports As New List(Of Airport)
    Dim selectedAirport As Airport

    Sub LoadInitialAirports()

        airports.Clear()

        Dim lpl As Airport = New Airport()
        lpl.code = "LPL"
        lpl.name = "Liverpool John Lennon"
        lpl.departure = True
        lpl.distance1 = 0
        lpl.distance2 = 0

        Dim boh As Airport = New Airport()
        boh.code = "BOH"
        boh.name = "Bournemouth International"
        boh.departure = True
        boh.distance1 = 0
        boh.distance2 = 0


        Dim int As Airport = New Airport()
        int.code = "INT"
        int.name = "International"
        int.departure = False
        int.distance1 = 0
        int.distance2 = 0

        airports.Add(lpl)
        airports.Add(boh)
        airports.Add(int)

    End Sub


    Sub Main()

        Call Sub() PrintWelcome()

        Call Sub() LoadInitialAirports()

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
                    Console.WriteLine("Invalid option: " + selection)
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
                            Console.WriteLine("You have selected " + airport.name + " as your start")
                            back = True
                        Else
                            Console.WriteLine("You have selected " + airport.name + " which is invalid departure location")
                        End If

                    End If

                Next
            End If


        Loop


        'If uk_code = "LPL" Then

        'Console.WriteLine("You have selected Liverpool John Lennon as your start")

        'ElseIf uk_code = "BOH" Then

        'Console.WriteLine("You have selected Bournemouth International as your start")

        'Else

        'Console.WriteLine("Code is invalid")
        'Call Sub() PrintMenu()

        'End If

        'Console.WriteLine("Please enter the three-letter airport code for your destination")





        'Console.WriteLine("if you wish to go back, type 0 in menu selection")

        'back = Console.ReadLine

        'If back = 0 Then

        'Call Sub() PrintMenu()

        'End If


    End Sub

    Sub Flight_details()

        Dim back As String

        Console.WriteLine("under construction, flight details")
        Console.WriteLine("if you wish to go back, type 0 in menu selection")

        back = Console.ReadLine

        If back = 0 Then

            Call Sub() PrintMenu()

        End If


    End Sub

    Sub Price_profit()

        Dim back As String

        Console.WriteLine("under construction, price - profit analysis")
        Console.WriteLine("if you wish to go back, type 0 in menu selection")

        back = Console.ReadLine

        If back = 0 Then

            Call Sub() PrintMenu()

        End If


    End Sub

    Sub Clear_data()

        Dim back As String

        Console.WriteLine("under construction, clear data")
        Console.WriteLine("if you wish to go back, type 0 in menu selection")

        back = Console.ReadLine

        If back = 0 Then

            Call Sub() PrintMenu()

        End If


    End Sub


End Module

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

    Class Aircraft
        Public type As String
        Public name As String
        Public costPer100km As Integer
        Public flightRange As Integer
        Public capacity As Integer
        Public minFirstClass As Integer
    End Class

    Dim airports As New List(Of Airport)
    Dim distances As New Dictionary(Of String, Dictionary(Of String, Distance))
    Dim aircrafts As New List(Of Aircraft)

    Dim selectedDepartureAirport As Airport
    Dim selectedArivalAirport As Airport
    Dim selectedDistance As Distance
    Dim selectedAircraft As Aircraft
    Dim selectedFirstClassSeats As Integer
    Dim selectedSecondClassSeats As Integer
    Dim selectedSeatsCapacity As Integer


    Dim codes As Boolean = False
    Dim aircraftTypes As Boolean = False
    Dim firstClassSeats As Boolean = False
    Dim flightPossible As Boolean = False
    Dim maxFlights As Integer = 0


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

        Call Sub() LoadAircraftType()

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
        If (Not selectedDepartureAirport Is Nothing) Then
            Console.WriteLine("   - Departure: " & selectedDepartureAirport.code)
            If (Not selectedArivalAirport Is Nothing) Then
                Console.WriteLine("   - Arrival: " & selectedArivalAirport.code)
                If (Not selectedDistance Is Nothing) Then
                    Console.WriteLine("   - Distance: " & selectedDistance.distanceKm & "km")
                End If
            End If
        End If
        Console.WriteLine("2. Enter flight details")
        If (Not selectedAircraft Is Nothing) Then
            Console.WriteLine("  - Aircraft type: " & selectedAircraft.name)
            Console.WriteLine("  - Maximum flight range: " & selectedAircraft.flightRange & "km")
            Console.WriteLine("  - Available capacity: " & selectedAircraft.capacity)
            Console.WriteLine("  - 1st Class: " & selectedFirstClassSeats)
            Console.WriteLine("  - 2nd Class: " & selectedSecondClassSeats)
            Console.WriteLine("  - Calculated capacity: " & selectedSeatsCapacity)
        End If
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
                Return
            Else

                For i = 1 To airports.Count

                    Dim airport = airports(i - 1)
                    If airport.code.ToLower = code.ToLower Then
                        If airport.departure Then
                            selectedDepartureAirport = airport
                            Console.WriteLine("You have selected " & airport.name & " as your start")
                            back = True
                            Exit For
                        End If
                    End If
                Next
                If back = False Then
                    Console.WriteLine("You have selected " & code & " which is invalid departure location")
                End If
            End If


        Loop

        back = False

        Do While back = False

            Console.WriteLine("Please enter the three-letter airport code for your arrival or 5 to exit to main menu")

            code = Console.ReadLine

            If code = "5" Then
                Return
            Else

                For i = 1 To airports.Count

                    Dim airport = airports(i - 1)
                    If airport.code.ToLower = code.ToLower Then
                        If Not airport.departure Then
                            selectedArivalAirport = airport
                            selectedDistance = getDistanceForSelectedAirports()
                            Console.WriteLine("You have selected " & airport.name & " as your start")
                            back = True
                            Exit For
                        End If
                    End If
                Next
                If back = False Then
                    Console.WriteLine("You have selected " & code & " which is invalid arrival location")
                End If
            End If

        Loop

    End Sub

    Function getDistanceForSelectedAirports() As Distance

        If (Not selectedDepartureAirport Is Nothing) Then
            If (Not selectedArivalAirport Is Nothing) Then
                Dim valueDestinations As Dictionary(Of String, Distance)
                distances.TryGetValue(selectedDepartureAirport.code, valueDestinations)
                Dim valueDistance As Distance
                valueDestinations.TryGetValue(selectedArivalAirport.code, valueDistance)
                Return valueDistance
            End If
        End If

        Return Nothing

    End Function

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
        Call Flight_details_body()
        Call Flight_details_Capacity()
    End Sub

    Sub Flight_details_body()

        Dim back As Boolean = False
        Dim type As String

        Do While back = False

            Console.WriteLine("please enter the type of aircraft that will be used for the flight or 5 to exit to the main menu")

            For i = 1 To aircrafts.Count

                Dim aircraft = aircrafts(i - 1)
                Console.WriteLine(" " & aircraft.type & " - " & aircraft.name)

            Next

            type = Console.ReadLine

            If type = "5" Then
                Return
            Else
                For i = 1 To aircrafts.Count

                    Dim aircraft = aircrafts(i - 1)
                    If aircraft.type.ToLower = type.ToLower Then

                        selectedAircraft = aircraft
                        Console.WriteLine("You have selected " & aircraft.name & " as your Aircraft")
                        back = True
                        Exit For
                    End If
                Next
                If back = False Then
                    Console.WriteLine("You have selected " & type & " which is invalid Aircraft")
                End If
            End If
        Loop

    End Sub

    Sub Flight_details_Capacity()

        Dim back As Boolean = False
        Dim seats As Integer

        Dim seatsFirst As Integer
        Dim seatsStand As Integer
        Dim capacity As Integer

        Do While back = False

            Console.WriteLine("Please enter the number first class seats or press 0 to have all seats be standard")

            Try
                seats = Console.ReadLine
            Catch ex As Exception
                Console.WriteLine("Invalid input...")
                Continue Do
            End Try

            capacity = selectedAircraft.capacity

            If seats <> 0 Then
                If seats >= selectedAircraft.minFirstClass Then
                    seatsFirst = seats
                    seatsStand = capacity - seatsFirst * 2
                Else
                    Console.WriteLine("Invalid input... Minimum number of first class seats is " & selectedAircraft.minFirstClass)
                    Continue Do
                End If
            ElseIf seats = 0 Then
                seatsFirst = 0
                seatsStand = selectedAircraft.capacity
            End If

            selectedFirstClassSeats = seatsFirst
            selectedSecondClassSeats = seatsStand
            selectedSeatsCapacity = seatsStand + seatsFirst
            back = True

        Loop



    End Sub

    Sub LoadAircraftType()

        Dim MediumNarrowBody As New Aircraft()
        MediumNarrowBody.type = "MNB"
        MediumNarrowBody.name = "Medium narrow body"
        MediumNarrowBody.costPer100km = 8
        MediumNarrowBody.flightRange = 2650
        MediumNarrowBody.capacity = 180
        MediumNarrowBody.minFirstClass = 8

        Dim LargeNarrowBody As New Aircraft()
        LargeNarrowBody.type = "LNB"
        LargeNarrowBody.name = "Large narrow body"
        LargeNarrowBody.costPer100km = 7
        LargeNarrowBody.flightRange = 5600
        LargeNarrowBody.capacity = 220
        LargeNarrowBody.minFirstClass = 10

        Dim MediumWideBody As New Aircraft()
        MediumWideBody.type = "MWB"
        MediumWideBody.name = "Medium wide body"
        MediumWideBody.costPer100km = 5
        MediumWideBody.flightRange = 4050
        MediumWideBody.capacity = 406
        MediumWideBody.minFirstClass = 14

        aircrafts.Add(MediumNarrowBody)
        aircrafts.Add(LargeNarrowBody)
        aircrafts.Add(MediumWideBody)

    End Sub

    Sub Price_profit()
        If isInfoValid() Then
            Console.WriteLine("All information is valid")
            Call Sub() ProfitCalculation()
        End If
    End Sub

    Function isInfoValid() As Boolean

        If (selectedDepartureAirport Is Nothing) Then
            Console.WriteLine("Error: No departure airport has been selected")
            Return False
        End If
        If (selectedArivalAirport Is Nothing) Then
            Console.WriteLine("Error: No arrival airport has been selected")
            Return False
        End If
        If (selectedAircraft Is Nothing) Then
            Console.WriteLine("Error: No aircraft has has been selected")
            Return False
        End If
        If (selectedAircraft Is Nothing) Then
            Console.WriteLine("Error: Number of First and standard class has not been entered")
            Return False
        End If
        If (selectedDistance Is Nothing Or selectedAircraft.flightRange < selectedDistance.distanceKm) Then
            Console.WriteLine("Error: Aircraft doesn't have enough fuel to reach destination")
            Return False
        End If
        Return True
    End Function

    Sub ProfitCalculation()

        Dim priceStandard As Integer
        Dim priceFirst As Integer

        Console.WriteLine("please enter the price of first and standard class seats")

    End Sub

    Sub Clear_data()

        Console.WriteLine("under construction, clear data")

    End Sub


End Module
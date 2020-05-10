# KafkaTest
I wanted to test if a single consumer subscribed to many topics would chew up lots of ports. Spoilers, it doesn't.

Most of the code is just the [Kafka library sample code](https://github.com/confluentinc/confluent-kafka-dotnet).

While both apps are running I ran the following PowerShell script to count the number of ports being used by KTConsume.

`Get-NetTCPConnection | Group-Object -Property State, OwningProcess | Select -Property Count, Name, @{Name="ProcessName";Expression={(Get-Process -PID ($_.Name.Split(',')[-1].Trim(' '))).Name}}, Group | Sort Count

My testing in Windows showed that running a test with 1, 10, and 100 topics, it always used 8 ports.

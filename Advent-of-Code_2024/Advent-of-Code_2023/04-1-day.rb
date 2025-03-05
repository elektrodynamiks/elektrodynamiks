# Open file and make an array wiht line break as separation.
#file_data = File.read("04-1-in.txt")
file = File.open("04-1-in.txt")

sample = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"

$points = 0

file.each_line do |line|
  array = line.split("\n").map { |ar| ar.split(":") }
  array = array.flatten.map { |ar| ar.split("|") }
  array.flatten!
  p array = array.drop(1)
  winning = array[0].split
  numbers = array[1].split
  winning
  numbers
  winners = (winning & numbers)
  if winners.count != 0
    p winners
    p $points += 2 ** (winners.count - 1)
  end
end

p "--Grand Total--"
p $points

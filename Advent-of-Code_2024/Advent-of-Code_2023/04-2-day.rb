# Open file and make an array wiht line break as separation.
#file_data = File.read("04-1-in.txt")
file = File.open("04-1-in.txt")

sample = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"

file_ = "Card   1: 82 41 56 54 18 62 29 55 34 20 | 37 14 10 80 58 11 65 96 90  8 59 32 53 21 98 83 17  9 87 25 71 77 70 73 24
Card   2:  3 18 94 53 23 49 60 44 67  9 | 77 10 44 41 17 23 83 60 49 43 94 76 67 87 66  9 73 29  3 33 92 53 18  6 47
Card   3: 85  9 33 66 70 28 91 60 96 65 | 33 56 83 65 15 23 99 60 26 66  9 70 97 11 38  1 85 96 28 59 50 91 71 48 89
Card   4: 51  5 25 18 53 30 43 49 91 21 | 21 48  1 85 51 80 18 16  9 87 66  3 14 13 39 34  5 70 69 49 28 37 72  6 98
Card   5: 32 62 99 93 37 22 64 57 94 55 | 23  1 26 15 16  6 88  4 17 69 21 46 49 70 51 45 89 91 29 52 60 86 80  8 12
Card   6: 44 52 33 82  8 30 32 62 26 61 | 59 10 89 41 24 56 48 70 92 20  3 17 94 85 97 42  8 93 51 57 44 13 12 63 78
Card   7: 71 42 27 38 36 41 95 97 34 10 | 61 36 10 38 95 71 53 99 59 88 50 72 40 27  3 78 41 28 16 42 48 54  6 82 97
Card   8: 88 80 86 60  7 77 72 29 55 36 | 91 15  8  3 16 59 70 10 90 77 56 48 22 95 78 69 94  9 38 23 35  1 17 39  7
Card   9: 79 32 28 61 34 19 71 47 87  5 | 19 87 47 14  3 68 25 71  5 65 91 89 98 36 24 34 61 38 80 32 62 28 74 79  2
Card  10: 18 95 55  5 98 22 68 70 74 92 |  5 39 92 95 36 65 98 88 70 22  3 68 45 25 15 61 63 52 74 14 55 18 17 60 47"

$points = {}

$array = []

k = 1
file.each_line do |line|
  array = line.split("\n").map { |ar| ar.split(":") }
  array = array.flatten.map { |ar| ar.split("|") }
  array.flatten!
  card = "card_#{k}"
  winning = array[1].split
  numbers = array[2].split
  p card
  p mypoint = $points[card].nil? ? 1 : ($points[card] + 1)
  p winning
  p numbers
  p (winning & numbers)
  p winners = (winning & numbers).count
  $points[card] = mypoint
  if winners >= 1
    winners.times do
      card = card.next
      if $points[card].nil?
        $points[card] = mypoint
      else
        $points[card] += mypoint
      end
    end
  end
  p $points
  k += 1
end

$missed = 0
$count = 0
p "--Grand Total--"

$points.each do |k, v|
  $count += v
end
p $points
p $count

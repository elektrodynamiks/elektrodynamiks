sample = "Time:      7  15   30
Distance:  9  40  200"
race = "Time:        44     82     69     81
Distance:   202   1076   1138   1458"

array = []
race.each_line do |line|
  array << line.split(" ").map { |s| s.to_i }
end
p array
races = []
n = array[0].count - 1
i = 1
n.times do
  races << [array[0][i], array[1][i]]
  i += 1
end
races
score = []
array = []
races.each do |race|
  p race
  t = race[0]
  d = race[1]
  arr = []
  t.times do |hold|
    t.times do |seconds|
      time = (t - seconds - hold == 0)
      if time
        racing = (hold * seconds > d)
        arr << [hold, seconds] if racing
      end
    end
  end
  score << arr.size
  array << arr
end
p array
p score
sc = 1
score.each do |s|
  sc *= s
end
p sc

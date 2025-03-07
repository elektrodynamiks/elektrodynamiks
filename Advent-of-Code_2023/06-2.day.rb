sample = "Time:      7  15   30
Distance:  9  40  200"
race = "Time:        44     82     69     81
Distance:   202   1076   1138   1458"

#array = []
#race.each_line do |line|
#  line = line.split(" ").flatten.map { |s| s.to_i }
#  p line = line.join.to_i
#  array << line
#end

race = [44826981, 202107611381458]
p race
t = race[0]
d = race[1]

$arr = 0

start = 5085566
k = 1
t.times do |hold|
  p hold = hold * k + start
  t.times do |s|
    seconds = t - s
    time = (t - seconds - hold == 0)
    if time
      racing = (hold * seconds <= d)
      #p [hold, seconds] if racing
      $arr += 1 if racing
      return unless racing
    end
  end
  p $arr
end

p t - 2 * $arr

def down(t, d)
  t.times do |hold|
    h = t - hold
    t.times do |s|
      seconds = s
      time = (t - seconds - h == 0)
      if time
        racing = (h * seconds <= d)
        p [h, seconds] if racing
        $arr += 1 if racing
        return unless racing
      end
    end
    p $arr
  end
end

up(t, d)
#down(t, d)
#p t - $arr
[44826981, 202107611381458]
5085566
1
5085567
44826981 - 2 * 5085567 + 1
=> 34655848

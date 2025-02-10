total = 0
sample = [
  "ndlnhscmrx53six58",
  "xmktfbmqdgndhcvbpb93trvgv",
  "52",
  "two1nine",
  "eightwothree",
  "abcone2threexyz",
  "xtwone3four",
  "4nineeightseven2",
  "zoneight234",
  "7pqrstsixteen",
]

digit_ = { "one" => 1, "two" => 2, "three" => 3, "four" => 4, "five" => 5, "six" => 6, "seven" => 7, "eight" => 8, "nine" => 9 }

pattern = /\d*(one){1}*\d*(two){1}*\d*(three){1}*\d*(four){1}*\d*(five){1}*\d*(six){1}*\d*(seven){1}*\d*(eight){1}*\d*(nine){1}*\d/

letters = /(one|two|three|four|five|six|seven|eight|nine)/i

sample.each do |s|
  p s
  a = s.match(/\d/)[0] if s.match?(/\d/)
  b = s.match(letters)[0] if s.match?(letters)
  d = a if a
  d = b if b
  if a && b
    d = a if s.index(a) <= s.index(b)
    d = b if s.index(a) >= s.index(b)
  end
  d = d.to_i unless digit_.key?(d.to_s)
  d = digit_[d.to_s] if digit_.key?(d.to_s)
  p d
end

nrettap = /(enin){1}*\d*(thgie){1}*\d*(neves){1}*\d*(xis){1}*\d*(evif){1}*\d*(ruof){1}*\d*(eerht){1}*\d*(owt){1}*\d*(eno){1}/i

srettel = /(enin|thgie|neves|xis|evif|ruof|eerht|owt|eno)/i
tigit_ = { "enin" => 9, "thgie" => 8, "neves" => 7, "xis" => 6, "evif" => 5, "ruof" => 4, "eerht" => 3, "owt" => 2, "eno" => 1 }

sample.each do |s|
  p s = s.reverse
  e = s.match(/\d/)[0] if s.match?(/\d/)
  f = s.match(srettel)[0] if s.match?(srettel)

  g = e if e
  g = f if f
  if e && f
    g = e if s.index(e) <= s.index(f)
    g = f if s.index(e) >= s.index(f)
  end

  if tigit_.key?(g.to_s)
    g = tigit_[g.to_s]
  else
    g = g.to_i
  end
  p g
end

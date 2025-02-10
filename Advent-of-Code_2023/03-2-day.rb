# Open file and make an array wiht line break as separation.
file_data = File.read("03-1-in.txt").split
# p file_data

sample = [
  "467..114..",
  "...*......",
  "..35..633.",
  "......#...",
  "617*......",
  ".....+.58.",
  "..592.....",
  "......755.",
  "...$.*....",
  ".664.598..",
]

test = [
  "12.......*..",
  "+.........34",
  ".......-12..",
  "..78........",
  "..*....60...",
  "78.........9",
  ".5.....23..$",
  "8...90*12...",
  "............",
  "2.2......12.",
  ".*.........*",
  "1.1..503+.56",
]

$chars = '<([{\^-=$!|]})?*+>!@#$%^&*()_+?/'
$star = "*"
pattern = /(\d)(\d)*(\d)*/i
d1 = /(\d\d*\d*)/
s1 = /\*/

k = 0
$count = 0

def matches(str, pattern)
  arr = []
  while (str && (m = str.match pattern))
    offset = m.offset(0).first
    arr << offset + (arr[-1] ? arr[-1] + 1 : 0)
    str = str[(offset + 1)..-1]
  end
  arr
end

def isGear(st, a0, a1, a2)
  # line before
  $isgear = []
  $array0 = []
  $array1 = []
  $array2 = []

  if !a0.nil?
    a0.each do |arr|
      array1 = []
      arr[1].each do |ar|
        proximity = (st[1].first.to_i - ar.to_i).abs
        a = 0 <= proximity && proximity <= 1
        array1 << arr[0] if a
      end
      $isgear << array1.uniq
    end
  end
  if !a2.nil?
    a2.each do |arr|
      array2 = []
      arr[1].each do |ar|
        proximity = (st[1].first.to_i - ar.to_i).abs
        b = 0 <= proximity && proximity <= 1
        array2 << arr[0] if b
      end
      $isgear << array2.uniq
    end
  end
  a1.each do |arr|
    array3 = []
    arr[1].each do |ar|
      proximity = (st[1].first.to_i - ar.to_i).abs
      c = proximity == 1
      array3 << arr[0] if c
    end
    $isgear << array3.uniq
  end

  return $isgear
end

file_data.each do
  input = file_data
  row0 = (k == 0) ? [] : input[k - 1]
  row1 = input[k]
  row2 = (k == input.count - 1) ? [] : input[k + 1]

  if row0.length != 0
    num0 = row0.scan(d1).flatten
    res0 = matches(row0, pattern)
    arr0 = []
    num0.each do |num|
      arr0 << [num, res0.slice!(0, num.length)]
    end
  end
  num1 = row1.scan(d1).flatten

  res1 = matches(row1, pattern)
  arr1 = []
  num1.each do |num|
    arr1 << [num, res1.slice!(0, num.length)]
  end
  if row2.length != 0
    num2 = row2.scan(d1).flatten
    res2 = matches(row2, pattern)
    arr2 = []
    num2.each do |num|
      arr2 << [num, res2.slice!(0, num.length)]
    end
  end

  stars = row1.scan(/\*/)
  gears = matches(row1, /\*/)
  star = []
  stars.each do |num|
    star << [num, gears.slice!(0, num.length)]
  end
  if stars.length != 0
    # for each star check row 1

    star.each do |st|
      isGear(st, arr0, arr1, arr2)
      $isgear.flatten!
      p $isgear if $isgear.count == 2
      ($count += ($isgear[0].to_i * $isgear[1].to_i)) if $isgear.count == 2
    end
  end
  k += 1
end

p $count

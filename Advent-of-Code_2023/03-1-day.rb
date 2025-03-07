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
point = "."
pattern = /(\d)(\d)*(\d)*/i
d1 = /(\d\d*\d*)/

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

def isRow1Valid(nb, pos, row1)
  valide = false
  l = pos.count
  lg = row1.length
  a = $chars.include?(row1[pos[0] - 1]) unless pos[0] == 0
  b = $chars.include?(row1[pos[0] + l]) unless (pos[0] + l) == lg
  # p row1 if a || b
  return nb.to_i if a || b
  return nil
end

def isRowNValid(nb, pos, row)
  valid = false
  return if row.nil?
  valide = false
  l = pos.count
  lg = row.length
  pos.each do |i|
    valid = $chars.include?(row[i])
    return nb.to_i if valid
  end
  a = $chars.include?(row[pos[0] - 1]) unless pos[0] == 0
  b = $chars.include?(row[pos[0] + l]) unless (pos[0] + l) == lg
  return nb.to_i if a || b
  return nil
end

file_data.each do
  input = file_data
  row0 = (k == 0) ? [] : input[k - 1]
  row1 = input[k]
  row2 = (k == input.count - 1) ? [] : input[k + 1]
  nums = row1.scan(d1).flatten
  res = matches(row1, pattern)
  srow = res
  frow = res
  nums.each do |number|
    index = srow.slice!(0, number.length)
    a = isRow1Valid(number, index, row1)
    if row2.length != 0
      b = isRowNValid(number, index, row2)
    end
    if row0.length != 0
      c = isRowNValid(number, index, row0)
    end
    $count += a if a
    $count += c if c
    $count += b if b
  end
  k += 1
end

p $count

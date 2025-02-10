file_data = File.read("19-in.txt")
#p file_data.count

test = "
px{a<2006:qkq,m>2090:A,rfg}
pv{a>1716:R,A}
lnx{m>1548:A,A}
rfg{s<537:gd,x>2440:R,A}
qs{s>3448:A,lnx}
qkq{x<1416:A,crn}
crn{x>2662:A,R}
in{s<1351:px,qqz}
qqz{s>2770:qs,m<1801:hdj,R}
gd{a>3333:R,R}
hdj{m>838:A,pv}

{x=787,m=2655,a=1222,s=2876}
{x=1679,m=44,a=2067,s=496}
{x=2036,m=264,a=79,s=2244}
{x=2461,m=1339,a=466,s=291}
{x=2127,m=1623,a=2188,s=1013}"

workflow, parts = test.split("\n\n")

def flow(flow)
  variable = flow.slice!(0)
  rule = flow.slice!(0)
  number = flow.match(/\d*/)[0]
  escape = flow.split(":").last
  return { variable: variable.to_sym, rule: rule, number: number.to_i, escape: escape.to_sym }
end

def workflow(lines)
  flow = lines.split("{")
  key = flow.first.to_sym
  flow = flow.last.split(",")
  processes = flow
  escape = flow.last.split("}").first.to_sym
  processes.delete_at(-1)
  conditions = []
  processes.each do |proc|
    conditions << flow(proc)
  end
  return { key => conditions << escape }
end

# create hash for functions definition from workflow
processes = {}
workflow.split.each do |flow|
  # in{s<1351:px,qqz}
  # {in: [[:s, "<", 1351,:px]],'qqz']
  processes.update workflow(flow)
end
p processes

all_parts = []
parts.split.each do |part|
  part.slice!(0)
  part.slice!(-1)
  part_hash = {}
  part.split(",").each do |variables|
    var = variables[0].to_sym
    value = variables.scan(/\d+/).first.to_i
    part_hash.merge!(var => value)
  end
  all_parts << part_hash
end

$keep_part = []

def check_workflow(part, process)
  p process
  process.each do |check|
    if check.class == Hash
      parameter = check[:variable]
      rule = check[:rule]
      case rule
      when "<"
        p part[parameter]
        p "<"
        p check[:number]
        return $starts << check[:escape] if part[parameter] < check[:number]
      when ">"
        p part[parameter]
        p ">"
        p check[:number]
        return $starts << check[:escape] if part[parameter] > check[:number]
      end
    elsif check.class == Symbol
      return if check == :R
      return $accept << part if check == :A
      return $starts << check if check != :A
    end
  end
end

$accept = []

all_parts.each do |part|
  $starts = [:in]
  loop do
    flow = $starts.pop
    if flow == :A
      $accept << part
    elsif flow != :R
      process = processes[flow]
      check_workflow(part, process)
    else
      break
    end
    break if $starts.empty?
  end
end

count = 0
$accept.each do |part|
  total = 0
  part.each do |key, val|
    total += val
  end
  count += total
end
p count

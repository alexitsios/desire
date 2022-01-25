VAR acquired_arm = false
VAR acquired_leg = false
VAR stern_door_open = false
VAR acquired_tool = false
VAR acquired_service_kit = false
VAR ship_is_sinking = false
VAR stern_visited = false
VAR funnel_visited = false
VAR superstructure_out_visited = false
 
== stern_load ==
>> setscene Stern
{
    - !stern_visited:
        >> settrapped true
        >> fadein 2
        >> sfx led_boot true
        $LED Left "001"
        $VACUUM_ROBOT Right "002"
        $LED Left "003"
        $VACUUM_ROBOT Right "004"
        ~ stern_visited = true
        -> DONE
    - !ship_is_sinking && acquired_service_kit:
        >> screenshake true
        Led "No! Don't sink now!"
        >> screenshake false
        Led "Any memories might help right now. But I need a drive for that..."
        ~ ship_is_sinking = true
        -> DONE
    - else: 
        -> DONE
}

== garbage_bin ==
>> bgchange trash_on_ground
VacuumRobot "Neeeeeaaaaahhhh! M-m-more dirt! More tra-tra-trash! Nev-never finish!"
>> moveto VacuumRobot TrashBin
-> DONE

== vacuum_robot ==
{
    - !acquired_leg:
        Led "Got it!"
        >> sfx puzzle_complete true
        Led "There, that’s a lot better! Great to be on my own two feet again. Now I need to find my arm"
        VacuumRobot "Dirt! Dirt! Dirt! So much dirt! Dir dir dir dir dirt! No- no time time time time time-"
        ~ acquired_leg = true
        >> settrapped false
        -> DONE
    - else:
        VacuumRobot "Ev- ev- everything sssssso dirty. No nada nope nein na time time time no time!"
        Led "Are you alright? You’re malfunctioning pretty bad, aren’t you?"
        VacuumRobot "Dirty- e-e-e-everything’s dirty. You-you’re d-d-dirty. I’m dirty- how did I g-g-get d-dirty? Dirt dirt dirt no time dirt-"
        Led "My name’s Led. Can- can you remember that? Led"
        VacuumRobot "L-Led. Led"
        Led "That's right, Led"
        VacuumRobot "Led is d-d-dirty!"
        Led "Huh. He’s pretty badly damaged. Poor thing must be so confused. His voice box is definitely screwed up"
        VacuumRobot "Cleandirtydirtyclean must dirty everything it’s too clean no-no that’s wrong no time no time"
        Led "Hum. I wonder if I can use the trash to get him to focus"
        Led "Hey- hey, look at this. Can you see this drinks can I’ve got here? Yes, that’s it. Do you know what this is?"
        VacuumRobot "Tr-tr-trash, Led"
        Led "Yes, yes that’s right. Well, there’s a trash can right here- would you like me to put the trash in the can?"
        VacuumRobot "Yes yes y-y-yes- clean, tidy. Nice and clean please Led not dirty, don’t have time"
        Led "Alright, good. Well, I can put it in there, but first, I want to ask you some questions. If you answer them, I’ll put it in the trashcan. Deal?"
        VacuumRobot "D- d- d- dea- DIRT! Deal"
        Led "Great, looks like I've got his attention. What should I ask?"
        -> vacuum_robot_ask_table
}

== vacuum_robot_ask_table ==
    * [You keep saying you don't have time. Why not? What's happening?] 
        -> ask_what_happened
    * [Do- do you know what happened? Why we're both damaged?] 
        -> why_are_we_damaged
    * [Do you know where the humans are?]
        -> where_are_the_humans
    * [I don't think I have any questions] 
        -> end_discussion_with_vacuum_robot

== ask_what_happened ==
Led "You keep saying you don’t have time. Why not? What’s happening?"
VacuumRobot "The- the shhhhip. It’s g-g-going..."
Led "Going where?"
VacuumRobot "D-do-dow-DIRT! DIRTY!"
Led "Hum. Guess I’m not getting that answer. Maybe I should try something else"
-> vacuum_robot_ask_table

== why_are_we_damaged ==
Led "Do- do you know what happened? Why we’re both damaged?"
VacuumRobot "I-I-I’m a cleaning robot. No defence. No a-a-attack. Only clean. No help"
Led "What?"
VacuumRobot "I c-c-couldn’t hhhhhhelp"
Led "Right. Okay. Um… I wonder if I can ask anything else"
-> vacuum_robot_ask_table

== where_are_the_humans ==
Led "Do you know where the humans are?"
VacuumRobot "N-n-n-no, no humans. No humans but all their trash, al-al-always the wwwway with humans. Ev-ev-everywhere they go they leave tr-tr-trash"
Led "You don’t know either, huh? Well, maybe you know something else"
-> vacuum_robot_ask_table

== end_discussion_with_vacuum_robot ==
Led "I don’t think I have any questions"
VacuumRobot "I have tra-tra trash so much trash to cl-cl-clear"
Led "Yeah, I can see that"
VacuumRobot "There is so much so much So Much SO MUCH SO MUCH SO MUCH SOOOOOOOOOOOOO MUUUUUUUUUUUUUCH!"
Led "Are you ok?"
VacuumRobot "..."
VacuumRobot "..."
VacuumRobot "So… so much dirt. So much t-t-trash, Never ge-e-et it cleaned in time"
Led "Hey? Hello? Are you okay?"
VacuumRobot "So unclean. You-you’re sssssso dirty. Who are you?"
Led "What? It’s me, Led, remember?"
VacuumRobot "Dirty!"
Led "Drat. What could have happened to you? Something terrible. Same thing that happened to me probably. I just wish I knew what it was"
Led "Well, I’m not going to find answers with him I guess. What else is there?"
-> DONE

== data_pad ==
Led "A Data Pad. Could’ve gotten a lot out of this, but it’s cracked and flickering. Hold on…"
>> bgchange data_pad
> Hey. Word from Captain says we've got to double security
More security? Why? What's wrong? <
> I don't ask questions, man. Just get the security bots working
Led "More security? What-"
>> bgchange data_pad
Led "Damn, it's turned off. Well, that's all I'm going to get"
-> DONE

== reflection ==
Led "Wow. I didn't realize how damaged I was"
Led "My arm's gone, my head module is damaged- it looks like someone's attacked me?"
Led "Wait, is that rust? How long have I been here?"
Led "Well, staring at my reflection won't help. Let's see what else I can find"
-> DONE

== service_bot ==
Led "It must have broken apart like that what it fell over. Maybe that's what happened to me"
Led "Huh. Even its internals are damaged. What could have happened to him? And the rest of them?"
Led "They're all so dead and lifeless- but I am not. I wish I could remember..."
Led "Ug. They are too badly damaged, I can’t access them"
Led "Cold... Unmoving... So lifeless. And the rust..."
-> DONE

== all_in_one_tool ==
Led "An all-in-one tool! And it still works! Unlike my missing arm..."
Led "Wait, that robot's arm is similar to mine- and undamaged"
Led "That’s it! Maybe I can use its arm to replace my own!"
>> additem AllInOneTool
~ acquired_tool = true
-> DONE

== arm ==
>> fadeout 1
>> changesprite Led led_with_arm
>> fadein 1
>> sfx puzzle_complete true
Led "...Yes I have it. And it moves, perfect"
    ~ acquired_arm = true
Led "If our arms are swappable... Maybe its memory logs could be as well..."
Led "Ah, but this tool isn't sophisticated enough for that. But I'll still keep it. Just in case"
-> DONE

== horizon ==
{
    - !ship_is_sinking:
        Led "There’s... Nothing. Only worlds of ocean"
        Led "I—I’ve done this before, looked up at the moon. The memory feels important but..."
        Led "I can't... Ugh... I can't remember! My Memory Drive fails me"
        {
            - acquired_arm:
                Led "Both arms and legs fully functioning... But I don’t feel whole"
        }
        Led "My memory- I need to find a way to restore it"
        Led "Maybe there are answers somewhere on this ship"
        -> DONE
    - else:
        Led "Maybe I'll find something here"
        Led "Oh no! The lower part of the ship is broken, there's a big hole- there's water going into the ship!"
        -> DONE
}


== door ==
{
    - !stern_door_open:
        Led "There's a door over there"
        Led "I've searched everywhere out here but I still need more tools. Maybe there's something out there"
        Led "The door is bolted shut. That panel next to it looks important, but it's damaged"
}
-> DONE

== pannel ==
{
    - !acquired_tool:
        Led "The pannel to open the doors. They're too damaged to be used, but maybe I can fix it somehow"
    - !stern_door_open && acquired_tool:
        Led "I might be able to hotwire it if I get the right angle... Oh! The all-in-one tool might be able to work here!"
        {
            - acquired_arm:
                Led "If it can attach an arm, it can fix a door. It just might take a few seconds"

            - else:
                Led "But with only one arm it would be impossible to work on such a complex system"
        }
}
-> DONE

== fix_pannel ==
{
    - !acquired_arm:
        Led "I'm sure I can use this tool to fix the pannel, but not with an arm missing"
        -> DONE
}
Led "Hum, the electronic inverter isn’t re-connecting – it must have been out for a while..."
Led "I can use this wire to trip – yes. That’s got it. I just need to wait to hear it click and then –"
Led "Oh shoot, my one tool, it's all busted up! At least the door is unlocked now, I just need to reconnect this bit and..."
>> removeitem AllInOneTool
>> bgchange stern_door_open
>> sfx puzzle_complete true
Led "A human. First one I’ve seen since I woke up"
Led "Hello! How may I be of –Oh. Oh, you're dead. Jeeze, it looks like you've been there for a while"
Led "Poor Human. Without anyone to serve it as well"
Led "Its fingers are burned, but I can’t tell what caused its death"
Led "This Engineer’s Uniform is badly decomposed. There's a logo, but I can barely make it out"
Led "But I don't recognize it. Hm. How long have we been here for?"
~ stern_door_open = true
>> quest OpenSternDoor Completed
-> DONE

== dead_cleaning_robot ==
Led "No signs of life. Even the broom has started to rot. What could have happened to it? To us. Maybe I can check its computer banks, see if it remembers something"
Led "Hmmm... Maybe I can... No, there’s more damage than I thought. Nothing here to salvage"
Led "It must have been dead for a while, with all that rust. I wonder what its last thought was"
Led "And why is it still cleaning? Whatever happened to it- to us... Must have happened all at once"
{
    - !acquired_arm:
        Led "Hum, I wonder... I might be able to use its arm to replace mine"
        Led "Uh... It won’t budge. So many dead robots. But I am still here. Why?"
}
-> DONE

== recover_memory ==
Led "Let me open up your head, my friend"
Led "Looks like it'll still work. I'm sorry, buddy"
Led "I’ve robbed you of your arm, and now I'm about to strip you off who you are..."
Led "Your very essence. Your memories. I shouldn't..."
Led "But it can't be helped. I hope you understand"
>> sfx puzzle_complete true
Led "It works, my friend. Thanks. I think I can remember some...thing..."
Led "I- I remember something about... watching the moon. with... a boy. My- my master?"
Led "He- he didn't like traveling, he was telling me... but- but his father wanted him with him"
Led "He was launching a new ship and wanted to show off his little family unit"
Led "And the boy didn't want to- didn't want to be a 'prop', he said"
Led "And he said- he said that I was built to be there when his father could not"
Led "And then he asked me about the moon. Asked if it would be around forever"
Led "'Of course, master. Of course. No matter what, the moon will always be there'"
Led "'Always be there in the darkness of the night. Even if you can't see it'"
Led "Then... then he said he wanted to go to the moon"
Led "'Well, sir, then I think one day you'll go to the moon'"
Led "'I think you'll do whatever you set your mind to'"
Led "That’s... that’s it? That’s all the memory drive can do"
Led "Ug. I need extensive repairs to access everything. I need..."
Led "Wait- I remember something else"
Led "I remember being paralyzed. Unable to move- for some reason"
Led "And- and my master- he was being dragged off. He was screaming, struggling"
Led "Then he was dragged into a lifeboat. I wanted to go with him. I had to go with him"
Led "But- but I can't. I can't, my systems are malfunctioning. All- all I can do is..."
Led "Fall. I fell, staring up at the moon. The quiet moon. Hanging above me"
Led "Then everything went black"
-> DONE

== item_use_error ==
Led "Hmmm... I can't use that here"
-> DONE

== funnel_load ==
>> setscene Funnel
{
    - !funnel_visited:
        Led "There’s so many bones- human bones. Enough for at least two fully grown ones"
        Led "That worktable might have something useful on it"
        ~ funnel_visited = true
}
-> DONE

== worktable ==
Led "Useless... useless... useless. Everything is worn out and useless..."
{
    - !acquired_service_kit:
        Led "Ooh! That might not be useless"
        >> additem ServiceKit
        ~ acquired_service_kit = true
}
-> DONE

== letter ==
Unknown "... know that I love you all dearly. You're all daddy's little angels."
Unknown "And to my wife, I'm sorry it's not me who came back to you. Know that thinking of you, my darling, is all that's keeping me going."
Unknown "But I can't survive on memories alone. My love for you encompasses the universe. I hope one day, in another lifetime, we can be together again."
Unknown "And to whoever finds this letter- If you ever make it out, give this letter to my family, if you find them."
Unknown "Tell them I wish I could’ve seen them. And... if you don't get out of here, well then, the sea would’ve won."
-> DONE

== bones ==
Led "Strange- it looks like there are cuts in these bones"
Led "I don't like this... Why are there just bones? Especially when the body by the door is intact"
Led "And where are these cuts from"
Led "It looks like something or someone killed these people. And... cut them up purposefully?"
-> DONE

== funnel_data_pad ==
Led "Looks like Daily Logs from the workers. Maybe reading some will prove useful"
Unknown "Kyle, Mark and Randy here. We were working here in the Funnel Room when it happened. We got stuck. There was no way to escape..."
Led "Ugh, I can barely make out the rest of the entry"
Unknown "... Kyle died. And we just had to... We were so hungry. He would not have minded, I swear. God forgive me"
-> DONE

== clothes ==
Led "Two sets of clothing. Ripped and stained. What's it by- oh... oh no... What did you do? What happened here?"
-> DONE

== superstructure_out_load ==
>> setscene Superstructure_out
{
    - !superstructure_out_visited:
        Led "The more my memory returns, the more questions I have..."
        Led "Maybe the superstructure ahead will hold some answers"
        ~ superstructure_out_visited = true
}
-> DONE

== superstructure_left_door ==
Led "Ugh, the door seems wedged. If it wasn't for my new arm, I think I’d fit..."
Led "But why is it half-open? All the other doors had either been left open or auto-locked… Maybe it's being blocked"
>> fadeout 1
>> fadein 1
Led "Oh! Another casualty... maybe if I can use my foot to anchor against the frame and then pull..."
Led "There you go, you’re free now at least. But a little too late"
Led "Even I’m looking worse for wear from that, my new arm is a little scratched and dented"
Led "Although my original arm is good as new... Odd"
Led "I guess I shouldn’t complain"
-> DONE

== empty_lifeboat ==
Led "The humans would head for the lifeboats... And they’re now missing"
Led "I really hope that my master made it off the boat"
Led "Oh? All of the lifeboats have gone... No, there’s one still here!"
-> DONE

== remaining_lifeboat ==
Led Left "Hum, it’s all locked up. I wonder why the humans didn’t... Crap!... Don’t shoot me I’m just a service bot!"
-> security_bot

== security_bot ==
SecurityBot Right "Vacate the area. Now"
Led Left "I-I- just saw another Security bot back there-"
SecurityBot "You will be forcibly removed if you do not comply with Emergency deck restrictions 6.55&#35;. You have 7 seconds to comply, or you will be forcibly de-activated."
Led "Wait! What’s the problem officer? I didn’t mean to- no, please don’t deactivate me! I just wanted to check the lifeboats for..."
SecurityBot "Under Emergency protocol 6.55&#35; no bot is allowed to access the lifeboats"
Led "Look, given the current state of the ship, we will need all the lifeboats we can get!"
SecurityBot "Under Emergency protocol 6.55&#35; no bot is allowed to access the lifeboats"
Led "Listen the ship is sinking. Right now. Emergency protocol 565... 465... Doesn’t matter!"
SecurityBot "6.55&#35;"
Led "Listen! The ship will sink. It IS sinking! We have to... I don’t think you understand!"
SecurityBot "False, I understand Emergency protocol 6.55&#35; excellently. It is programmed into my internal hard drive and cannot be edited or corrupted.  In the event of an emergency – listed in subsection 6b – that a lifeboat or vessel is required-"
Led "Yes, I got that bit, but I just need to-"
SecurityBot "- only human personnel will be allowed onto lifeboats. Bots will only be able to gain admittance to lifeboats if-"
Led "Yes, that’s me I-"
SecurityBot "-they are either accompanied by a human or have A16 clearance. Do you meet either of these necessary criteria?"
Led "No. I mean, I don’t know if I have A16 clearance, I don’t remember if maybe..."
SecurityBot "Please hold... Scanning"
Led "Thank you. That’s all I wanted-"
SecurityBot "Scan complete. You do not"
Led "But I’m trying to find my human master! Doesn’t that count for something?"
SecurityBot "False. Incompatible clearance"
Led "I really don't want to sink! Surely that’s the whole point of-"
SecurityBot "False. Incompatible clearance"
Led "I can’t even remember what's going on, and I’m trying to find my master and not sink on this ship, and I’ve seen so many poor bots who... I just really need your help! Can you not even comprehend what I’m talking about?"
SecurityBot "Yes. You have no A16 clearance and under section 6.55&#35; you will be forcibly de-activated if you do not vacate the area in 7 seconds"
Led "Thank goodness I am programmed with reason. Security bots must be programmed to follow orders at all costs. Not hugely helpful when there’s no humans around to give the orders"
Led "I don’t even know what to do now..."
Led "Wait, maybe I can find a human to give me that A16 clearance! Surely the Captain could help"
Led "Wait, you’ve been standing outside here the whole time?"
SecurityBot "My programming has assigned me to this post"
Led "So, do you know what happened here?"
SecurityBot "My emergency protocol was auto-activated but I do not know the events that initiated this"
Led "But how are you still alive? I’ve found so many dead robots, what happened to them?"
SecurityBot "My emergency protocol programming requires me to rescue all humans, not bots"
Led "Did you see what happened to them though?"
SecurityBot "I am required to make sure all human personnel escaped the ship, to guard the lifeboats for more survivors and to await further orders"
Led "You didn’t even notice the bots? Fine. But there are no more humans. Or no living ones at least. I haven’t found any trace of them..."
SecurityBot "I must await further orders and guard for survivors"
Led "But this boat is sinking. There are no humans left. You’re going to die"
SecurityBot "I must await further orders and guard for survivors at all costs"
Led "Why won’t you listen? Although, your head unit is severely damaged. The protective layers look like almost disintegrated"
Led "Your AI core is exposed, you must have suffered a great deal of damage to your internal logic centers and have to rely on its base programming"
Led "Look... Can I fix that for you? There are enough parts around here that I could patch-"
SecurityBot "False, my internal programming cannot be edited or corrupted and only official human personnel are authorized to tamper with my unit or my software"
Led "But you’re leaking oil, that’ll corrode your eletronics soon and then you won’t –"
SecurityBot "False, my internal programming cannot be edited or corrupted and only official human personnel are authorized to tamper with my unit or my software"
Led "... If you’re sure. It’s not like I can get through to you anyway"
Led "I suppose I’ll head back to the superstructure and try and find the bridge. Maybe I’ll find the captain or more information about the missing lifeboats"
-> DONE